using MagicVilla_VillaAPI.Models.DTO;

namespace MagicVilla_VillaAPI.Data
{
    public class VillaStore
    {

        public static List<VillaDTO> VillaList = new List<VillaDTO>
        {
            new VillaDTO { Id = 1, Name = "Pool View"},
            new VillaDTO { Id = 2, Name = "Beachfront"},
            new VillaDTO { Id = 3, Name = "Mountain Retreat" }
        };
    }
}
