using ProductManager.CommonComponents;
using ProductManager.DTOModels;
using ProductManager.DTOModels.Product;
using ProductManager.DTOModels.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services
{
    public static class Validators
    {
        public record struct ValidationError(string ErrorMessage, string MemberName);
        public static List<ValidationError> Validate(this IProductValidateDTO productCandidate)
        {
            var errors = new List<ValidationError>();
            if (productCandidate.WarehouseId == Guid.Empty)
                errors.Add(new ValidationError("Product must be assigned to a warehouse.", nameof(ProductCreateDTO.WarehouseId)));
            errors.AddRange(ValidateProduct(productCandidate.Name, productCandidate.Description, productCandidate.Category, productCandidate.Quantity.ToString(), productCandidate.Price.ToString()));
            return errors;
        }
        public static List<ValidationError> ValidateProduct(string name, string description, Category? category, string quantityText, string priceText)
        {
            var errors = new List<ValidationError>();
            errors.AddRange(ValidateString(name, nameof(IProductValidateDTO.Name), "Name"));
            errors.AddRange(ValidateString(description, nameof(IProductValidateDTO.Description), "Description"));
            errors.AddRange(ValidateString(quantityText, "QuantityText", "Quantity"));
            errors.AddRange(ValidateString(priceText, "PriceText", "Price"));
            errors.AddRange(ValidateString(category.GetDisplayName(), nameof(IProductValidateDTO.Category), "Category"));
            return errors;
        }
        public static List<ValidationError> Validate(this IWarehouseValidateDTO warehouseCandidate)
        {
            var errors = new List<ValidationError>();
            errors.AddRange(ValidateWarehouse(warehouseCandidate.Name, warehouseCandidate.Location));
            return errors;
        }
        public static List<ValidationError> ValidateWarehouse(string name, Location? location)
        {
            var errors = new List<ValidationError>();
            errors.AddRange(ValidateString(name, nameof(IWarehouseValidateDTO.Name), "Name"));
            errors.AddRange(ValidateString(location.GetDisplayName(), nameof(IWarehouseValidateDTO.Location), "Location"));
            return errors;
        }
        private static List<ValidationError> ValidateString(string value, string propertyName, string displayName)
        {
            var errors = new List<ValidationError>();
            if (String.IsNullOrWhiteSpace(value))
            {
                errors.Add(new ValidationError($"{displayName} can't be empty.", propertyName));
                return errors;
            }
            return errors;
        }
    }
}
