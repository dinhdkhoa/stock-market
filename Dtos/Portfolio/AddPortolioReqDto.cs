using System.ComponentModel.DataAnnotations;

namespace stock_market.Dtos.Portfolio
{
    public class AddPortolioReqDto
    {
        [Required]
        [MaxLength(4, ErrorMessage = "Symbol cannot be over 4 over characters")]
        public string Symbol { get; set; } = string.Empty;
    }
}
