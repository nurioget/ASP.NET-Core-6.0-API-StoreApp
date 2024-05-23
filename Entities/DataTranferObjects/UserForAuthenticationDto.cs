using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTranferObjects
{
    public record UserForAuthenticationDto
    {
        [Required(ErrorMessage ="Username is required")]
        public String? UserName { get; init; }

        [Required(ErrorMessage ="Password is required")]
        public String? Password { get; init; }

    }
}
