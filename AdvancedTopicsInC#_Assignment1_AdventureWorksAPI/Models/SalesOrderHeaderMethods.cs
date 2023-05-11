using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models
{
    public class SalesOrderHeaderMethods
    {
        public static IResult CreateSalesOrder(AdventureWorksLt2019Context db, SalesOrderHeader salesOrder)
        {
            try
            {
                db.Add(salesOrder);
                db.SaveChanges();

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }

        }

        public static IResult Read(AdventureWorksLt2019Context db, int? id)
        {
            if (id != null)
            {
                SalesOrderHeader? salesOrderHeader = db.SalesOrderHeaders.Find(id);

                if (salesOrderHeader == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(salesOrderHeader);
            }
            else
            {
                List<SalesOrderHeader> salesOrderHeaders = db.SalesOrderHeaders.ToList();
                return Results.Ok(salesOrderHeaders);
            }

        }

        public static IResult RemoveSalesOrder(AdventureWorksLt2019Context db, int id)
        {
            SalesOrderHeader salesOrder = db.SalesOrderHeaders.Find(id);

            if (salesOrder == null)
            {
                return Results.NotFound();
            }

            db.SalesOrderHeaders.Remove(salesOrder);
            db.SaveChanges();

            return Results.Ok();

        }

        public static IResult UpdateSalesOrder(AdventureWorksLt2019Context context, int Id, SalesOrderHeader? salesOrderHeader)
        {
            
            try
            {
                SalesOrderHeader? CurrentSale = context.SalesOrderHeaders.Where(s => s.SalesOrderId == Id).FirstOrDefault();


                if (CurrentSale == null && salesOrderHeader != null)
                {
                    CreateSalesOrder(context, salesOrderHeader);

                    return Results.Created($"/salesOrderHeader?id={salesOrderHeader.SalesOrderId}", salesOrderHeader);
                }
                else if (CurrentSale != null)
                {
                    CurrentSale.RevisionNumber = salesOrderHeader.RevisionNumber;
                    CurrentSale.DueDate = salesOrderHeader.DueDate;
                    CurrentSale.ShipDate = salesOrderHeader.ShipDate;
                    CurrentSale.Status = salesOrderHeader.Status;
                    CurrentSale.AccountNumber = salesOrderHeader.AccountNumber;
                    CurrentSale.ShipMethod = salesOrderHeader.ShipMethod;
                    CurrentSale.SubTotal = salesOrderHeader.SubTotal;
                    CurrentSale.TaxAmt = salesOrderHeader.TaxAmt;
                    CurrentSale.Freight = salesOrderHeader.Freight;
                    CurrentSale.TotalDue = salesOrderHeader.TotalDue;
                    CurrentSale.Comment = salesOrderHeader.Comment;
                    CurrentSale.Rowguid = Guid.NewGuid();
                    CurrentSale.ModifiedDate = DateTime.Now;


                    context.SalesOrderHeaders.Update(CurrentSale);
                    context.SaveChanges();

                    Read(context, CurrentSale.SalesOrderId);
                    return Results.Ok(CurrentSale);
                }
                return Results.Ok();

            }
            catch (Exception ex)
            {
                return Results.BadRequest();
            }
        }

    }
}
