using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{


    [Authorize]
    public class MessagesController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        public MessagesController(IUserRepository userRepository, IMessageRepository messageRepository,IMapper mapper)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
            _userRepository = userRepository;


        }
        [HttpPost]

        public async Task<ActionResult<MessageDto>> CreateMessage( CreateMessageDto createMessageDto)
        {
            var username =User.GetUsername();
            if(username ==createMessageDto.RecipientUsername.ToLower())
             return BadRequest("you cant send to you");

             var sender = await _userRepository.GetUserByUsernameAsync(username);
             var recipient =await _userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);


             if(recipient == null ) return NotFound();

             var message = new Message
             {
                 Sender =sender,
                 Recipient =recipient,
                 SenderUsername = sender.Value.UserName,
                 RecipientUsername =recipient.Value.UserName,
                 Content =createMessageDto.Content,
             };
             _messageRepository.AddMessage(message);

             if(await _messageRepository.SaveAllAsync())  return Ok( _mapper.Map<MessageDto>(message));


             return BadRequest("sending is failed");
        }
    }
    
}