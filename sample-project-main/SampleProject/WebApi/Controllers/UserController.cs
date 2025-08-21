using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Users;
using WebApi.Models.Users;

namespace WebApi.Controllers
{
    [RoutePrefix("users")]
    public class UserController : BaseApiController
    {
        private readonly ICreateUserService _createUserService;
        private readonly IDeleteUserService _deleteUserService;
        private readonly IGetUserService _getUserService;
        private readonly IUpdateUserService _updateUserService;

        public UserController(ICreateUserService createUserService, IDeleteUserService deleteUserService, IGetUserService getUserService, IUpdateUserService updateUserService)
        {
            _createUserService = createUserService;
            _deleteUserService = deleteUserService;
            _getUserService = getUserService;
            _updateUserService = updateUserService;
        }

        [Route("{userId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateUser(Guid userId, [FromBody] UserModel model)
        {
            if (model == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Request body cannot be null." });
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            try
            {

                var user = _createUserService.Create(userId, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags);
                return Found(new UserData(user));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Exception while creating user :  "+ex.Message );
            }
        }


        [Route("{userId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateUser(Guid userId, [FromBody] UserModel model)
        {
            if (model == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Request body cannot be null.");
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {

                var user = _getUserService.GetUser(userId);
                if (user == null)
                {
                    return DoesNotExist();
                }
                _updateUserService.Update(user, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags);
                return Found(new UserData(user));
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while updating userinfo : {ex.Message}");
            }
        }

        [Route("{userId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteUser(Guid userId)
        {
            try
            {
                var user = _getUserService.GetUser(userId);
                if (user == null)
                {
                    return DoesNotExist();
                }
                _deleteUserService.Delete(user);
                return Found();
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while deleteing a user : {ex.Message}");
            }
        }

        [Route("{userId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetUser(Guid userId)
        {
            try
            {
                var user = _getUserService.GetUser(userId);
                return Found(new UserData(user));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while retreiving user details : {ex.Message}");
            }
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetUsers(int skip, int take, UserTypes? type = null, string name = null, string email = null)
        {
            try
            {

                var users = _getUserService.GetUsers(type, name, email)
                                           .Skip(skip).Take(take)
                                           .Select(q => new UserData(q))
                                           .ToList();
                return Found(users);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while retreiving users list : {ex.Message}");
            }

        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllUsers()
        {
            try
            {
                _deleteUserService.DeleteAll();
                return Found();
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while deleting all users data  : {ex.Message}");
            }
        }

        [Route("list/tag")]
        [HttpGet]
        public HttpResponseMessage GetUsersByTag(string tag)
        {
            try
            {
                var userInfo = _getUserService.GetUserByTag(tag);
                return Request.CreateResponse(HttpStatusCode.OK, userInfo);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error while retreiving user details by tag : {ex.Message}");
            }
        }


    }
}