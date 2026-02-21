using ProductManager.DBModels;
using ProductManager.CommonComponents;

namespace ProductManager.Services
{
    internal static class FakeStorage
    {
        private static readonly List<WarehouseDBModel> _warehouses;
        private static readonly List<ProductDBModel> _products;

        internal static IEnumerable<WarehouseDBModel> Warehouses
        {
            get
            {
                return _warehouses.ToList();
            }
        }
        internal static IEnumerable<ProductDBModel> Products
        {
            get
            {
                return _products.ToList();
            }
        }

        static FakeStorage()
        {
            var warehouseKyiv = new WarehouseDBModel("Kyiv Warehouse", Location.Kyiv);
            var warehouseKhmelnytsky = new WarehouseDBModel("Khmelnytsky Warehouse", Location.Khmelnytsky);
            var warehouseLviv = new WarehouseDBModel("Lviv Warehouse", Location.Lviv);
            _warehouses = new List<WarehouseDBModel> { warehouseKyiv, warehouseKhmelnytsky, warehouseLviv };
            _products = new List<ProductDBModel>
            {
                new ProductDBModel(warehouseKyiv.Id, "1993 Fender Telecaster TC72", 1, 1500, Category.ElectricGuitars, "1993 Fender Japan '72 Telecaster Custom (TC72) in Ocean Turquoise Metallic (OTM)—the rare early FujiGen iteration of the color,\nwhich is much darker than the common CIJ iteration."),
                new ProductDBModel(warehouseKyiv.Id, "Marshall JCM 900", 2, 1150, Category.Amplifiers, "Marshall JCM 900 model 4102 amp. 2 X G12T-75 Speakers.\n100watt combo. Recently serviced. Great Sound"),
                new ProductDBModel(warehouseKyiv.Id, "Digitech FreqOut", 6, 150, Category.GuitarPedals, "The DigiTech FreqOut Natural Feedback Creator allows you to get sweet, natural feedback at any volume, with or without distortion.\nThe FreqOut is perfect for situations where volume must be controlled like in the studio, with in ear monitors, or low-volume performance and practice."),
                new ProductDBModel(warehouseKyiv.Id, "EHX BadStone", 4, 80, Category.GuitarPedals, "Guitar effect pedal type phase shifter. It offers a phaser effect that adds a phase-shifted signal to the original signal."),
                new ProductDBModel(warehouseKyiv.Id, "Behringer Model 15", 10, 200, Category.Synthesizer, "The Behringer Model 15 Analog Semi-Modular Synthesizer stands out as a versatile and powerful tool for music production."),
                new ProductDBModel(warehouseKyiv.Id, "Behringer Deepmind 6", 3, 450, Category.Synthesizer, "Classic Analog Polysynth. With the DeepMind 12, Behringer set out to create an analog polysynth\nthat can effortlessly deliver any classic synth sound you've ever heard — or imagined."),
                new ProductDBModel(warehouseKyiv.Id, "Behringer PRO-1", 7, 150, Category.Synthesizer, "The PRO-1 is an analog synthesizer with dual oscillators, authentic sound based on the use of\nthe original circuits with the legendary 3340 and 3320 semiconductors,\na 4-pin VCF filter, 3-waveforms and very intuitive controls on the synthesizer's main panel."),
                new ProductDBModel(warehouseKyiv.Id, "Revoltage RV-40R", 11, 125, Category.Amplifiers, "Crank It Up to Eleven!. This powerhouse 40W guitar combo amplifier is built for serious players who\ndemand volume, versatility, and exceptional tone."),
                new ProductDBModel(warehouseKyiv.Id, "Bugera V22 Infinium", 1, 430, Category.Amplifiers, "Tube amplifier guitar combo. Hand-built 22-Watt guitar combo driven by\n2 x EL84 Tubes. World-famous, British engineered 12'' TURBOSOUND speaker."),
                new ProductDBModel(warehouseKyiv.Id, "Blackstar Studio 10", 5, 500, Category.Amplifiers, "The elegant Studio 10 EL34 is a low gain, classic British crunch amp with an EL34 in the\npower amplifier and an ECC83 in the preamp stage, resulting in a flawlessly boutique, sultry crunch."),
                new ProductDBModel(warehouseKhmelnytsky.Id, "Yamaha Pacifica 112J", 12, 200, Category.ElectricGuitars, "Superb value for money, the Pacifica 112J features a tried-and-tested design perfect for beginner guitarists."),
                new ProductDBModel(warehouseKhmelnytsky.Id, "Squier Classic Vibe '70s Stratocaster", 3, 600, Category.ElectricGuitars, "The Squier Classic Vibe '70s Stratocaster electric guitar creates an incredible sound thanks to Fender Designed Alnico Single-Coil pickups"),
                new ProductDBModel(warehouseKhmelnytsky.Id, "Fender American Pro II Stratocaster", 1, 2200, Category.ElectricGuitars, "This electric guitar delivers instant familiarity and sonic versatility that you'll immediately feel and hear,\nwith a wide range of enhancements that set nothing short of a new standard for professional instruments."),
                new ProductDBModel(warehouseLviv.Id, "Woodstock Standard Tele MN Vintage White", 1, 280, Category.ElectricGuitars, "The Woodstock Standard Tele guitar is designed for blues, indie rock, rock 'n' roll, and country.\nThe guitar's thick, bright tone and balanced character make it a versatile instrument for live and studio use."),
            };

        }
    }
}
