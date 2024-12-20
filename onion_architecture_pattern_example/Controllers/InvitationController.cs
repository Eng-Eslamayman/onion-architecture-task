using Application.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/invitations")]
[ApiController]
public class InvitationController : ControllerBase
{
    private readonly InvitationService _invitationService;

    public InvitationController(InvitationService invitationService)
    {
        _invitationService = invitationService;
    }

    [HttpGet("{mobileNumber}")]
    public async Task<IActionResult> CheckInvitation(string mobileNumber)
    {
        var hasInvitation = await _invitationService.CheckInvitationAsync(mobileNumber);
        return Ok(new { HasInvitation = hasInvitation });
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvitation([FromBody] CreateInvitationRequest request)
    {
        await _invitationService.SaveInvitationAsync(request.MobileNumber, request.InviterId);
        return Ok();
    }
}

public record CreateInvitationRequest(string MobileNumber, Guid InviterId);
