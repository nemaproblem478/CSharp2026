using System;
using System.ComponentModel.DataAnnotations;

namespace ProductManager.CommonComponents
{
    public enum Category
    {
        [Display(Name = "Electric Guitar")]
        ElectricGuitar,
        [Display(Name = "Guitar Pedal")]
        GuitarPedal,
        [Display(Name = "Amplifier")]
        Amplifier,
        [Display(Name = "Synthesizer")]
        Synthesizer,
    }
}
