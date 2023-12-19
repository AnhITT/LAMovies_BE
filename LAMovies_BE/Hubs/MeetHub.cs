using Microsoft.AspNetCore.SignalR;

namespace LAMovies_BE.Hubs
{
    public class MeetHub : Hub
    {
        public async Task JoinRoom(int roomId, string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
            await Clients.Group(roomId.ToString()).SendAsync("user-connected",userId);
        }
    }
}
