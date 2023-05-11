using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Data;

namespace AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models
{
    public class ProductMethods
    {
        public static IResult CreateProduct(AdventureWorksLt2019Context db, Product product)
        {
            try
            {
                db.Add(product);
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
                Product? product = db.Products.Find(id);
                if (product == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(product);
            }
            else
            {
                List<Product> products = db.Products.ToList();
                return Results.Ok(products);
            }

        }

        public static IResult RemoveProduct(AdventureWorksLt2019Context db, int id)
        {
            var product = db.Products.Find(id);

            if (product == null)
            {
                return Results.NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Results.Ok();

        }

        
        public static IResult UpdateProduct(AdventureWorksLt2019Context context, int Id, Product? product)
        {
            Product? selectedProduct = context.Products.Find(Id);

            try
            {
                if (selectedProduct == null && product != null)
                {

                    CreateProduct(context, product);
                    return Results.Created($"/product?id={product.ProductId}", product);
                }
                else if (selectedProduct != null)
                {
                    selectedProduct.Name = product.Name;
                    selectedProduct.ProductNumber = product.ProductNumber;
                    selectedProduct.Color = product.Color;
                    selectedProduct.StandardCost = product.StandardCost;
                    selectedProduct.ListPrice = product.ListPrice;
                    selectedProduct.SellStartDate = product.SellStartDate;
                    selectedProduct.Rowguid = Guid.NewGuid();
                    selectedProduct.ModifiedDate = DateTime.Now;

                    context.Products.Update(selectedProduct);
                    context.SaveChanges();

                    Read(context, selectedProduct.ProductId);
                    return Results.Ok(selectedProduct);
                }
                return Results.Ok();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
