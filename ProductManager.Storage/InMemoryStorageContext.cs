using ProductManager.CommonComponents;
using ProductManager.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Storage
{
    public class InMemoryStorageContext : IStorageContext
    {
        private record class WarehouseRecord(Guid Id, string Name, Location Location);
        private record class ProductRecord(Guid Id, Guid WarehouseId, string Name, int Quantity, double Price, Category Category, string Description);

        private static readonly List<WarehouseRecord> _warehouses = new List<WarehouseRecord>();
        private static readonly List<ProductRecord> _products = new List<ProductRecord>();

        static InMemoryStorageContext()
        {
            //Initializing storage
            var warehouseLypky = new WarehouseRecord(Guid.NewGuid(), "Lypky", Location.Kyiv);
            var warehouseTritone = new WarehouseRecord(Guid.NewGuid(), "Tritone", Location.Khmelnytsky);
            var warehouseZahid = new WarehouseRecord(Guid.NewGuid(), "Zahid", Location.Lviv);
            _warehouses.Add(warehouseLypky);
            _warehouses.Add(warehouseTritone);
            _warehouses.Add(warehouseZahid);
            _products.Add(new ProductRecord(Guid.NewGuid(), warehouseLypky.Id, "1993 Fender Telecaster TC72", 1, 1500, Category.ElectricGuitar, "1993 Fender Japan '72 Telecaster Custom (TC72) in Ocean Turquoise Metallic (OTM)—the rare early FujiGen iteration of the color,\nwhich is much darker than the common CIJ iteration."));
            _products.Add(new ProductRecord(Guid.NewGuid(), warehouseLypky.Id, "Marshall JCM 900", 2, 1150, Category.Amplifier, "Marshall JCM 900 model 4102 amp. 2 X G12T-75 Speakers.\n100watt combo. Recently serviced. Great Sound"));
            _products.Add(new ProductRecord(Guid.NewGuid(), warehouseLypky.Id, "Digitech FreqOut", 6, 150, Category.GuitarPedal, "The DigiTech FreqOut Natural Feedback Creator allows you to get sweet, natural feedback at any volume, with or without distortion.\nThe FreqOut is perfect for situations where volume must be controlled like in the studio, with in ear monitors, or low-volume performance and practice."));
            _products.Add(new ProductRecord(Guid.NewGuid(), warehouseLypky.Id, "EHX BadStone", 4, 80, Category.GuitarPedal, "Guitar effect pedal type phase shifter. It offers a phaser effect that adds a phase-shifted signal to the original signal."));
            _products.Add(new ProductRecord(Guid.NewGuid(), warehouseLypky.Id, "Behringer Model 15", 10, 200, Category.Synthesizer, "The Behringer Model 15 Analog Semi-Modular Synthesizer stands out as a versatile and powerful tool for music production."));
            _products.Add(new ProductRecord(Guid.NewGuid(), warehouseLypky.Id, "Behringer Deepmind 6", 3, 450, Category.Synthesizer, "Classic Analog Polysynth. With the DeepMind 12, Behringer set out to create an analog polysynth\nthat can effortlessly deliver any classic synth sound you've ever heard — or imagined."));
            _products.Add(new ProductRecord(Guid.NewGuid(), warehouseLypky.Id, "Behringer PRO-1", 7, 150, Category.Synthesizer, "The PRO-1 is an analog synthesizer with dual oscillators, authentic sound based on the use of\nthe original circuits with the legendary 3340 and 3320 semiconductors,\na 4-pin VCF filter, 3-waveforms and very intuitive controls on the synthesizer's main panel."));
            _products.Add(new ProductRecord(Guid.NewGuid(), warehouseLypky.Id, "Revoltage RV-40R", 11, 125, Category.Amplifier, "Crank It Up to Eleven!. This powerhouse 40W guitar combo amplifier is built for serious players who\ndemand volume, versatility, and exceptional tone."));
            _products.Add(new ProductRecord(Guid.NewGuid(), warehouseLypky.Id, "Bugera V22 Infinium", 1, 430, Category.Amplifier, "Tube amplifier guitar combo. Hand-built 22-Watt guitar combo driven by\n2 x EL84 Tubes. World-famous, British engineered 12'' TURBOSOUND speaker."));
            _products.Add(new ProductRecord(Guid.NewGuid(), warehouseLypky.Id, "Blackstar Studio 10", 5, 500, Category.Amplifier, "The elegant Studio 10 EL34 is a low gain, classic British crunch amp with an EL34 in the\npower amplifier and an ECC83 in the preamp stage, resulting in a flawlessly boutique, sultry crunch."));
            _products.Add(new ProductRecord(Guid.NewGuid(), warehouseTritone.Id, "Yamaha Pacifica 112J", 12, 200, Category.ElectricGuitar, "Superb value for money, the Pacifica 112J features a tried-and-tested design perfect for beginner guitarists."));
            _products.Add(new ProductRecord(Guid.NewGuid(), warehouseTritone.Id, "Squier Classic Vibe '70s Stratocaster", 3, 600, Category.ElectricGuitar, "The Squier Classic Vibe '70s Stratocaster electric guitar creates an incredible sound thanks to Fender Designed Alnico Single-Coil pickups"));
            _products.Add(new ProductRecord(Guid.NewGuid(), warehouseTritone.Id, "Fender American Pro II Stratocaster", 1, 2200, Category.ElectricGuitar, "This electric guitar delivers instant familiarity and sonic versatility that you'll immediately feel and hear,\nwith a wide range of enhancements that set nothing short of a new standard for professional instruments."));
            _products.Add(new ProductRecord(Guid.NewGuid(), warehouseZahid.Id, "Woodstock Standard Tele MN Vintage White", 1, 280, Category.ElectricGuitar, "The Woodstock Standard Tele guitar is designed for blues, indie rock, rock 'n' roll, and country.\nThe guitar's thick, bright tone and balanced character make it a versatile instrument for live and studio use."));
        }

        public async IAsyncEnumerable<WarehouseDBModel> GetWarehousesAsync()
        {
            foreach (var warehouse in _warehouses)
            {
                await Task.Delay(500);
                yield return new WarehouseDBModel(warehouse.Id, warehouse.Name, warehouse.Location);
            }
        }
        public async Task<WarehouseDBModel> GetWarehouseAsync(Guid warehouseId)
        {
            await Task.Delay(1000);
            var warehouse = _warehouses.FirstOrDefault(warehouse => warehouse.Id == warehouseId);
            return warehouse is null ? null : new WarehouseDBModel(warehouse.Id, warehouse.Name, warehouse.Location);

        }
        public async Task<IEnumerable<ProductDBModel>> GetProductsByWarehouseAsync(Guid warehouseId)
        {
            await Task.Delay(1000);
            return _products.Where(product => product.WarehouseId == warehouseId).Select(product => new ProductDBModel(product.Id, product.WarehouseId, product.Name, product.Quantity, product.Price, product.Category, product.Description));
        }
        public async Task<ProductDBModel> GetProductAsync(Guid productId)
        {
            await Task.Delay(1000);
            var product = _products.FirstOrDefault(product => product.Id == productId);
            //if returned product is null, return null. if not null, then return DBModel based on the found one
            return product is null ? null : new ProductDBModel(product.Id, product.WarehouseId, product.Name, product.Quantity, product.Price, product.Category, product.Description);

        }
        public async Task SaveProductAsync(ProductDBModel newProduct)
        {
            await Task.Delay(1000);
            int index = _products.FindIndex(p => p.Id == newProduct.ProductId);
            
            if (index != -1) //if found index for Product, replace old record with a new one
            {
                _products[index] = new ProductRecord(newProduct.ProductId, newProduct.WarehouseId, newProduct.Name, newProduct.Quantity, newProduct.Price, newProduct.Category, newProduct.Description);
            }
            else //if not, then add new record to the end of the list
            {
                _products.Add(new ProductRecord(newProduct.ProductId, newProduct.WarehouseId, newProduct.Name, newProduct.Quantity, newProduct.Price, newProduct.Category, newProduct.Description));
            }
        }
        public async Task<int> GetProductsByWarehouseCountAsync(Guid id)
        {
            await Task.Delay(500);
            return _products.Count(product => product.WarehouseId == id);
        }

        public async Task<double> GetWarehouseTotalCostAsync(Guid id)
        {
            await Task.Delay(500);
            var products = await GetProductsByWarehouseAsync(id);
            return products.Sum(p => p.Price *  p.Quantity);
        }
    }
}
