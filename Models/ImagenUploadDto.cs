namespace MandiraApi.Models {
    public class ImagenUploadDto {
        public int ProductoId { get; set; }

        public IFormFile Imagen { get; set; } = null!;
    }

}
