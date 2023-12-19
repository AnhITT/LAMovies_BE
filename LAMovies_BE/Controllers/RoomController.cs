using Libs.Models;
using Libs.Repositories;
using Libs.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace LAMovies_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository roomRepository;
        private readonly IUserInRoomRepository userInRoomRepository;

        public RoomController(IRoomRepository roomRepository, IUserInRoomRepository userInRoomRepository)
        {
            this.roomRepository = roomRepository;
            this.userInRoomRepository = userInRoomRepository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetAllActors()
        {
            var actors = roomRepository.getAll();
            return Ok(actors);
        }
        [HttpGet]
        [Route("GetRoomById")]
        public ActionResult<IEnumerable<Room>> GetRoomById(int id)
        {
            try
            {
                var data = roomRepository.GetById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete]
        public ActionResult DeleteRoom(int id)
        {
            try
            {
                var data = roomRepository.GetById(id);

                if (data == null)
                {
                    return NotFound("Genre not found");
                }

                roomRepository.Delete(data);
                roomRepository.Save();
                return Ok("Delete Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("deleteUserToRoom")]
        public ActionResult deleteUserToRoom(int roomId, string userId)
        {
            try
            {
                userInRoomRepository.Delete(roomId, userId);
                userInRoomRepository.Save();
                return Ok("Delete Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult insertRoom(string roomName, string userId)
        {
            try
            {
                Room room = new Room();
                room.Name = roomName;
                room.CountMember = 1;
                roomRepository.Insert(room);
                roomRepository.Save();

                UserInRoom userInRoom = new UserInRoom();
                userInRoom.UserId = userId;
                userInRoom.RoomId = room.Id;
                userInRoomRepository.Insert(userInRoom);
                userInRoomRepository.Save();
                return Ok(room.Id);
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });

            }
        }
        [HttpPost]
        [Route("addUserToRoom")]
        public IActionResult addUserToRoom(int roomId, string userId)
        {
            try
            {
                //todo: thêm user vào room (truy cập vào DB)
                UserInRoom userInRoom = new UserInRoom();
                userInRoom.UserId = userId;
                userInRoom.RoomId = roomId;
                userInRoomRepository.Insert(userInRoom);
                userInRoomRepository.Save();

                return Ok(new { status = true, message = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex });
            }
        }
        [HttpGet]
        [Route("getUserInRoom")]
        public IActionResult getUserInRoom(int roomId)
        {
            try
            {
                var userinroomList = userInRoomRepository.getUserInRoomList(roomId);
                return Ok(new { status = true, message = userinroomList });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

    }
}
