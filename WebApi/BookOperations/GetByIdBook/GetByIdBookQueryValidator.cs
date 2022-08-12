﻿using FluentValidation;

namespace WebApi.BookOperations.GetByIdBook
{
    public class GetByIdBookQueryValidator : AbstractValidator<GetByIdBookQuery>
    {
        public GetByIdBookQueryValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);



        }
    }
}