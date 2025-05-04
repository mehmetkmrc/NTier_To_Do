using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.ToDoAppNTier.Dtos.WorkDtos;

namespace Udemy.ToDoAppNTier.Business.ValidationRules
{
    public class WorkCreateDtoValidator : AbstractValidator<WorkCreateDto>
    {
        public WorkCreateDtoValidator()
        {
            RuleFor(x => x.Definition).NotEmpty();
                
                //.WithMessage("Definition is required").When(x=>x.IsCompleted).Must(NotBeMehmet).WithMessage("Definition cannot be Mehmet or mehmet");
        }

        //private bool NotBeMehmet(string arg)
        //{
        //    return arg != "Mehmet" && arg != "mehmet";
        //}
    }
}
