using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using WebApplication.Core.Users.Common.Models;
using WebApplication.Infrastructure.Interfaces;
using WebApplication.Infrastructure.Entities;

namespace WebApplication.Core.Users.Commands
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public int Id { get; set; }
        public string GivenNames { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;

        public class Validator : AbstractValidator<UpdateUserCommand>
        {
            private readonly IUserService _userService;
            public Validator(IUserService userService)
            {
                _userService = userService;
                bool IsUserExists(int userId)
                {
                    var user = _userService.GetAsync(userId);
                    if (user.Id > 0)
                        return true;

                    return false;
                }
                RuleSet("All", () =>
                {
                    RuleFor(user => user.Id).NotNull().WithMessage("id could not be null")
                             .GreaterThan(0).WithMessage("id must greater than 0")
                             .Must(IsUserExists);

                    RuleFor(user => user.GivenNames).NotNull().WithMessage("Given name is required")
                            .NotEmpty().WithMessage("Given name is required");

                    RuleFor(user => user.LastName).NotNull().WithMessage("Last name is required")
                            .NotEmpty().WithMessage("Last name is required");

                    RuleFor(user => user.EmailAddress).NotNull().WithMessage("Email is required")
                         .NotEmpty().WithMessage("Email is required");

                    RuleFor(user => user.MobileNumber).NotNull().WithMessage("Mobile number is required")
                   .NotEmpty().WithMessage("Mobile number is required");
                });

            
                // TODO: Create validation rules for UpdateUserCommand so that all properties are required.
                // If you are feeling ambitious, also create a validation rule that ensures the user exists in the database.
            }
       
        }

        public class Handler : IRequestHandler<UpdateUserCommand, UserDto>
        {
            private readonly IUserService _userService;
            private readonly IMapper _mapper;
            public Handler(IUserService userService, IMapper mapper)
            {
                _userService = userService;
                _mapper = mapper;
            }
    
            /// <inheritdoc />
            public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                User user = new User
                {
                    GivenNames = request.GivenNames,
                    LastName = request.LastName,
                    ContactDetail = new ContactDetail
                    {
                        EmailAddress = request.EmailAddress,
                        MobileNumber = request.MobileNumber
                    }
                };
                User updateUser = await _userService.UpdateAsync(user, cancellationToken);
                UserDto result = _mapper.Map<UserDto>(updateUser);

                return result;
                //throw new NotImplementedException("Implement a way to update the user associated with the provided Id.");
            }
        }
    }
}
