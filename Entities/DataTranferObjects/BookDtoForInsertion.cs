using System.ComponentModel.DataAnnotations;

namespace Entities.DataTranferObjects
{
    public record BookDtoForInsertion : BookDtoForManipulation
    {
        [Required(ErrorMessage ="CategoryId is required")]
        public int CategoryId { get; init; }
    }
}
