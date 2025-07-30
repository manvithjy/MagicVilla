using MagicVilla_VillaAPI.Models.DTO;

namespace MagicVilla_VillaAPI.Data
{
    public class VillaStore
    {

        public static List<VillaDTO> VillaList = new List<VillaDTO>
        {
            new VillaDTO { Id = 1, Name = "Pool View", Occupancy = 4, Sqft = 100},
            new VillaDTO { Id = 2, Name = "Beachfront", Occupancy = 6, Sqft = 150},
            new VillaDTO { Id = 3, Name = "Mountain Retreat", Occupancy = 10, Sqft = 200}
        };
    }
}
