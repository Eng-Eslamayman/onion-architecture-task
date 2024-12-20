using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

public class JwtAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IJwtVerifier _jwtVerifier;

    public JwtAuthMiddleware(RequestDelegate next, IJwtVerifier jwtVerifier)
    {
        _next = next;
        _jwtVerifier = jwtVerifier;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrEmpty(token))
        {
            var claimsPrincipal = _jwtVerifier.Verify(token);
            if (claimsPrincipal != null)
            {
                context.User = claimsPrincipal;
            }
        }

        await _next(context);
    }
}
