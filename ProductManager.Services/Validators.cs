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
            errors.AddRange(ValidateProduct(productCandidate.Name, productCandidate.Description, productCandidate.Category, productCandidate.Quantity, productCandidate.Price));
            return errors;
        }
        private static List<ValidationError> ValidateProduct(string name, string description, Category? category, int quantity, double price)
        {
            var errors = new List<ValidationError>();
            errors.AddRange(ValidateString(name, nameof(ProductCreateDTO.Name), "Name"));
            errors.AddRange(ValidateString(description, nameof(ProductCreateDTO.Description), "Description"));
            errors.AddRange(ValidateString(quantity.ToString(), nameof(ProductCreateDTO.Quantity), "Quantity"));
            errors.AddRange(ValidateString(price.ToString(), nameof(ProductCreateDTO.Price), "Price"));
            if (category == null)
            {
                errors.Add(new ValidationError("Product category must be selected.", nameof(ProductCreateDTO.Category)));
            }
            return errors;
        }
        public static List<ValidationError> Validate(this IWarehouseValidateDTO warehouseCandidate)
        {
            var errors = new List<ValidationError>();
            errors.AddRange(ValidateWarehouse(warehouseCandidate.Name, warehouseCandidate.Location));
            return errors;
        }
        private static List<ValidationError> ValidateWarehouse(string name, Location? location)
        {
            var errors = new List<ValidationError>();
            errors.AddRange(ValidateString(name, nameof(WarehouseCreateDTO.Name), "Name"));
            if (location == null)
            {
                errors.Add(new ValidationError("Warehouse must be assigned to a location.", nameof(WarehouseCreateDTO.Location)));
            }
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
