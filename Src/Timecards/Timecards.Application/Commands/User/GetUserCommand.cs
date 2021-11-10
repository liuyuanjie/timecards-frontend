using System;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;
using Timecards.Services.Impl;

namespace Timecards.Application.Commands.User
{
    public class GetUserCommand : IGetUserCommand
    {
        private readonly IUserService _userService;

        public GetUserCommand(IApiRequestFactory apiRequestFactory)
        {
            _userService = new UserService(apiRequestFactory);
        }

        public void LoginAsync(UserRequest userRequest, Action<ResponseBase<UserResult>> callbackProcess)
        {
            _userService.GetUserAsync(userRequest, (userRequestResponse) => callbackProcess(userRequestResponse));
        }
    }
}