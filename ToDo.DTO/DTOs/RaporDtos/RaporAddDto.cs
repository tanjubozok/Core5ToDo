using ToDo.Entities.Concrete;

namespace ToDo.DTO.DTOs.RaporDtos
{
    public class RaporAddDto
    {
        public string Tanim { get; set; }
        public string Detay { get; set; }
        public Gorev Gorev { get; set; }
        public int GorevId { get; set; }
    }
}
