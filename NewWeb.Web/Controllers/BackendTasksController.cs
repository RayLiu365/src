﻿using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using AutoMapper;
using NewWeb.Tasks;
using NewWeb.Tasks.Dtos;
using NewWeb.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NewWeb.Web.Controllers
{
    [AbpMvcAuthorize]
    public class BackendTasksController : NewWebControllerBase
    {
        private readonly ITaskAppService _taskAppService;
        private readonly IUserAppService _userAppService;

        public BackendTasksController(ITaskAppService taskAppService, IUserAppService userAppService)
        {
            _taskAppService = taskAppService;
            _userAppService = userAppService;            
        }

        // GET: Task
        public ActionResult List()
        {
            try
            {
                Logger.Debug("Start List action");
                ViewBag.TaskStateDropdownList = GetTaskStateDropdownList(null);
                var userList = _userAppService.GetUsers();
                ViewBag.AssignedPersonId = new SelectList(userList.Items, "Id", "Name");

                int countList = userList.Items.Count;
                Logger.Debug("AssignedPersonId:"+userList.Items[0].Id.ToString()+"  List count is:" + countList.ToString());                
            }
            catch(Exception e)
            {
                Logger.Error(e.Message);
            }
            return View();
        }

        [DontWrapResult]
        public JsonResult GetAllTasks(int limit, int offset, string sortfiled, string sortway, string search,
            string status)
        {
            Logger.Debug("Start GetAllTasks");
            var sort = !string.IsNullOrEmpty(sortfiled) ? string.Format("{0} {1}", sortfiled, sortway) : "";
            TaskState currentState;
            if (!string.IsNullOrEmpty(status))
                Enum.TryParse(status, true, out currentState);

            var filter = new GetTasksInput
            {
                SkipCount = offset,
                MaxResultCount = limit,
                Sorting = sort,
                Filter = search
            };

            if (!string.IsNullOrEmpty(status))
                if (Enum.TryParse(status, true, out currentState))
                    filter.State = currentState;

            var pagedTasks = _taskAppService.GetPagedTasks(filter);

            Logger.Debug("End GetAllTasks");
            return Json(new { total = pagedTasks.TotalCount, rows = pagedTasks.Items }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTaskInput task)
        {
            var id = _taskAppService.CreateTask(task);

            //return Json(id, JsonRequestBehavior.AllowGet);
            return RedirectToAction("List");
        }

        public PartialViewResult Edit(int id)
        {
            var task = _taskAppService.GetTaskById(id);

            var updateTaskDto = Mapper.Map<UpdateTaskInput>(task);

            var userList = _userAppService.GetUsers();
            ViewBag.AssignedPersonId = new SelectList(userList.Items, "Id", "Name", updateTaskDto.AssignedPersonId);

            return PartialView("_EditTask", updateTaskDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateTaskInput updateTaskDto)
        {
            if (ModelState.IsValid)
            {
                _taskAppService.UpdateTask(updateTaskDto);

                //return Json(true, JsonRequestBehavior.AllowGet);
                return RedirectToAction("List");
            }
            //return Json(false, JsonRequestBehavior.AllowGet);
            return View(updateTaskDto);
        }

        private List<SelectListItem> GetTaskStateDropdownList(TaskState? curState)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "AllTasks",
                    Value = "",
                    Selected = curState == null
                }
            };

            list.AddRange(Enum.GetValues(typeof(TaskState))
                .Cast<TaskState>()
                .Select(state => new SelectListItem
                {
                    Text = $"TaskState_{state}",
                    Value = state.ToString(),
                    Selected = state == curState
                })
            );

            return list;
        }
    }
}