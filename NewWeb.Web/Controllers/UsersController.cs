using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus.Handlers;
using Abp.UI;
using Abp.Web.Mvc.Authorization;
using NewWeb.Authorization;
using NewWeb.Authorization.Roles;
using NewWeb.Users;
using NewWeb.Web.Models.Users;

namespace NewWeb.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class UsersController : NewWebControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly RoleManager _roleManager;

        public UsersController(IUserAppService userAppService, RoleManager roleManager)
        {
            _userAppService = userAppService;
            _roleManager = roleManager;
        }

        public async Task<ActionResult> Index()
        {
            var users = (await _userAppService.GetAll(new PagedResultRequestDto { MaxResultCount = int.MaxValue })).Items; //Paging not implemented yet
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new UserListViewModel
            {
                Users = users,
                Roles = roles
            };

            return View(model);
        }

        public async Task<ActionResult> EditUserModal(long userId)
        {
            var user = await _userAppService.Get(new EntityDto<long>(userId));
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new EditUserModalViewModel
            {
                User = user,
                Roles = roles
            };
            return View("_EditUserModal", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserModalViewModel editUserModalViewModel)
        {
            if (ModelState.IsValid)
            {
                var input = editUserModalViewModel.User;
                Users.Dto.UpdateUserDto inputUser = new Users.Dto.UpdateUserDto
                {
                    Id = input.Id,
                    EmailAddress = input.EmailAddress,
                    IsActive = input.IsActive,
                    Name = input.Name,
                    RoleNames = input.Roles,
                    Surname = input.Surname,
                    UserName = input.UserName
                };
                _userAppService.Update(inputUser);
            }
            return View(editUserModalViewModel);
        }
    }

}