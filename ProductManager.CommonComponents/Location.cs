using System;
using System.ComponentModel.DataAnnotations;

namespace ProductManager.CommonComponents
{
    public enum Location
    {
        [Display(Name = "Kyiv")]
        Kyiv,
        [Display(Name = "Khmelnytsky")]
        Khmelnytsky,
        [Display(Name = "Lviv")]
        Lviv,
        [Display(Name = "Dnipro")]
        Dnipro,
        [Display(Name = "Odesa")]
        Odesa
    }
}
