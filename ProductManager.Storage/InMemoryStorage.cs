using ProductManager.CommonComponents;
using ProductManager.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Storage
{
    public class InMemoryStorage : IStorageContext
    {

        private record class WarehouseRecord(Guid Id, string Name, Location Location);
        private record class ProductRecord(Guid Id, Guid WarehouseId, string Name, int Quantity, double Price, Category Category, string Description);

        private static readonly List<WarehouseRecord> _warehouses = new List<WarehouseRecord>();
        private static readonly List<ProductRecord> _products = new List<ProductRecord>();

        static InMemoryStorage()
        {
            //Initializing storage
            var warehouseKyiv = new WarehouseRecord(Guid.NewGuid(), "Kyiv Warehouse", Location.Kyiv);
            var warehouseKhmelnytsky = new WarehouseRecord(Guid.NewGuid(), "Khmelnytsky Warehouse", Location.Khmelnytsky);
            var warehouseLviv = new WarehouseRecord(Guid.NewGuid(), "Lviv Warehouse", Location.Lviv);
            _warehouses = new List<WarehouseRecord> { warehouseKyiv, warehouseKhmelnytsky, warehouseLviv };
            _products = new List<ProductRecord>
            {
                new ProductRecord(Guid.NewGuid(), warehouseKyiv.Id, "1993 Fender Telecaster TC72", 1, 1500, Category.ElectricGuitar, "1993 Fender Japan '72 Telecaster Custom (TC72) in Ocean Turquoise Metallic (OTM)—the rare early FujiGen iteration of the color,\nwhich is much darker than the common CIJ iteration."),
                new ProductRecord(Guid.NewGuid(), warehouseKyiv.Id, "Marshall JCM 900", 2, 1150, Category.Amplifier, "Marshall JCM 900 model 4102 amp. 2 X G12T-75 Speakers.\n100watt combo. Recently serviced. Great Sound"),
                new ProductRecord(Guid.NewGuid(), warehouseKyiv.Id, "Digitech FreqOut", 6, 150, Category.GuitarPedal, "The DigiTech FreqOut Natural Feedback Creator allows you to get sweet, natural feedback at any volume, with or without distortion.\nThe FreqOut is perfect for situations where volume must be controlled like in the studio, with in ear monitors, or low-volume performance and practice."),
                new ProductRecord(Guid.NewGuid(), warehouseKyiv.Id, "EHX BadStone", 4, 80, Category.GuitarPedal, "Guitar effect pedal type phase shifter. It offers a phaser effect that adds a phase-shifted signal to the original signal."),
                new ProductRecord(Guid.NewGuid(), warehouseKyiv.Id, "Behringer Model 15", 10, 200, Category.Synthesizer, "The Behringer Model 15 Analog Semi-Modular Synthesizer stands out as a versatile and powerful tool for music production."),
                new ProductRecord(Guid.NewGuid(), warehouseKyiv.Id, "Behringer Deepmind 6", 3, 450, Category.Synthesizer, "Classic Analog Polysynth. With the DeepMind 12, Behringer set out to create an analog polysynth\nthat can effortlessly deliver any classic synth sound you've ever heard — or imagined."),
                new ProductRecord(Guid.NewGuid(), warehouseKyiv.Id, "Behringer PRO-1", 7, 150, Category.Synthesizer, "The PRO-1 is an analog synthesizer with dual oscillators, authentic sound based on the use of\nthe original circuits with the legendary 3340 and 3320 semiconductors,\na 4-pin VCF filter, 3-waveforms and very intuitive controls on the synthesizer's main panel."),
                new ProductRecord(Guid.NewGuid(), warehouseKyiv.Id, "Revoltage RV-40R", 11, 125, Category.Amplifier, "Crank It Up to Eleven!. This powerhouse 40W guitar combo amplifier is built for serious players who\ndemand volume, versatility, and exceptional tone."),
                new ProductRecord(Guid.NewGuid(), warehouseKyiv.Id, "Bugera V22 Infinium", 1, 430, Category.Amplifier, "Tube amplifier guitar combo. Hand-built 22-Watt guitar combo driven by\n2 x EL84 Tubes. World-famous, British engineered 12'' TURBOSOUND speaker."),
                new ProductRecord(Guid.NewGuid(), warehouseKyiv.Id, "Blackstar Studio 10", 5, 500, Category.Amplifier, "The elegant Studio 10 EL34 is a low gain, classic British crunch amp with an EL34 in the\npower amplifier and an ECC83 in the preamp stage, resulting in a flawlessly boutique, sultry crunch."),
                new ProductRecord(Guid.NewGuid(), warehouseKhmelnytsky.Id, "Yamaha Pacifica 112J", 12, 200, Category.ElectricGuitar, "Superb value for money, the Pacifica 112J features a tried-and-tested design perfect for beginner guitarists."),
                new ProductRecord(Guid.NewGuid(), warehouseKhmelnytsky.Id, "Squier Classic Vibe '70s Stratocaster", 3, 600, Category.ElectricGuitar, "The Squier Classic Vibe '70s Stratocaster electric guitar creates an incredible sound thanks to Fender Designed Alnico Single-Coil pickups"),
                new ProductRecord(Guid.NewGuid(), warehouseKhmelnytsky.Id, "Fender American Pro II Stratocaster", 1, 2200, Category.ElectricGuitar, "This electric guitar delivers instant familiarity and sonic versatility that you'll immediately feel and hear,\nwith a wide range of enhancements that set nothing short of a new standard for professional instruments."),
                new ProductRecord(Guid.NewGuid(), warehouseLviv.Id, "Woodstock Standard Tele MN Vintage White", 1, 280, Category.ElectricGuitar, "The Woodstock Standard Tele guitar is designed for blues, indie rock, rock 'n' roll, and country.\nThe guitar's thick, bright tone and balanced character make it a versatile instrument for live and studio use."),
            };
        }

        public IEnumerable<WarehouseDBModel> GetWarehouses()
        {
            foreach (var warehouse in _warehouses)
            {
                yield return new WarehouseDBModel(warehouse.Id, warehouse.Name, warehouse.Location);
            }
        }

        public IEnumerable<ProductDBModel> GetProductsByWarehouse(Guid warehouseId)
        {
            return _products.Where(product => product.WarehouseId == warehouseId).Select(product => new ProductDBModel(product.Id, product.WarehouseId, product.Name, product.Quantity, product.Price, product.Category, product.Description));
        }

        public int GetProductsByWarehouseCount(Guid id)
        {
            return _products.Count(product => product.WarehouseId == id);
        }
    }
}
