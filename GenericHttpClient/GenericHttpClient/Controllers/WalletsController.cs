using GenericHttpClient.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GenericHttpClient.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletsController : ControllerBase
{
    private readonly IWalletService _walletService;

    public WalletsController(IWalletService walletService)
    {
        _walletService = walletService;
    }

    // ------------------------------------------------------------
    // POST /api/wallets
    // ------------------------------------------------------------
    [HttpPost]
    public async Task<IActionResult> CreateWallet([FromBody] CreateWalletRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _walletService.CreateWalletAsync(request.Currency);

        return CreatedAtAction(nameof(GetBalance), new { walletId = id }, new { walletId = id });
    }

    // ------------------------------------------------------------
    // GET /api/wallets/{walletId}?currency={currency}
    // ------------------------------------------------------------
    [HttpGet("{walletId:guid}")]
    public async Task<IActionResult> GetBalance(Guid walletId, [FromQuery] string? currency)
    {
        var response = await _walletService.GetBalanceAsync(walletId, currency);
        return Ok(response);
    }

    // ------------------------------------------------------------
    // POST /api/wallets/{walletId}/adjustbalance?amount=&currency=&strategy=
    // ------------------------------------------------------------
    [HttpPost("{walletId:guid}/adjustbalance")]
    public async Task<IActionResult> AdjustBalance(
        Guid walletId,
        [FromQuery] decimal amount,
        [FromQuery] string currency,
        [FromQuery] string strategy)
    {
        await _walletService.AdjustBalanceAsync(walletId, amount, currency, strategy);
        return Ok(new { Message = "Balance updated successfully." });
    }
    
    public class CreateWalletRequest
    {
        public string Currency { get; set; } = default!;
    }
    
    public class WalletBalanceResponse
    {
        public Guid WalletId { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }

        public decimal? ConvertedBalance { get; set; }
        public string? ConvertedCurrency { get; set; }
    }
}