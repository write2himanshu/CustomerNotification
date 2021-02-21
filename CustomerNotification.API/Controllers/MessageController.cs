using CustomerNotification.Common;
using CustomerNotification.Common.Models;
using CustomerNotificaton.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace CustomerNotification.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : BaseController
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private IConfiguration configuration;

        /// <summary>
        /// Messaging Service
        /// </summary>
        private IMessagingService _messagingService;

        /// <summary>
        /// Message generator
        /// </summary>
        private IMessageGenerator messageGenerator;


        /// <param name="configuration"></param>
        /// <param name="messagingService"></param>
        public MessageController(IConfiguration configuration, IMessagingService messagingService, IMessageGenerator messageGenerator) : base(configuration)
        {
            this.configuration = configuration;
            _messagingService = messagingService;
            this.messageGenerator = messageGenerator;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [Route("/userRegister")]
        public async Task<IActionResult> RegisterUser([FromBody] UserModel request)
        {
            try
            {
                InitializeCorelation();

                if (!ModelState.IsValid || request == null)
                {
                    return BadRequest();
                }


                var response = messageGenerator.MessageProcessor(request, MessageType.NewUserRegistered);

                if (response.Length > 0)
                {
                    await _messagingService.SendMessageAsync(request.UserId, response);
                    return StatusCode(StatusCodes.Status200OK, $"User id: {request.UserId} successfully created");
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent, "User creation failed");
                }

            }
            catch (Exception ex)
            {
                LogError(MethodInfo.GetCurrentMethod().Name, $"Unhandled Exception occured. {ex.Message}", ex);
                return HandleException(ex);
            }
        }

    }
}
