using Microsoft.AspNetCore.Mvc;
using bettor.Models;
using bettor.Services;

namespace bettor.Controllers;

[ApiController]
[Route("bets")]
public class BetController : ControllerBase {
    private readonly ILogger<BetController> _logger;

    private readonly List<Bet> _bets = new List<Bet>();

    private readonly BetService _betService = BetService.getInstance();
    private readonly UserService _userService = UserService.getInstance();

    public BetController(ILogger<BetController> logger) {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Bet> GetAll() {
        return _betService.GetBets();
    }

    [HttpGet("{id}")]
    public ActionResult<Bet> GetBet(int id) {
        var bet = _betService.GetBet(id);

        if(bet == null)
            return NotFound();

        return bet;
    }

    [HttpPost]
    public IActionResult PlaceBet(Bet bet) {
        var user = _userService.GetUser(bet.UserId);

        if(user == null) {
            return BadRequest();
        }

        // TODO: verify account balance, verify number 0..9

        _betService.PlaceBet(user, bet);
        return CreatedAtAction(nameof(PlaceBet), new { id = bet.Id }, bet);
    }

    [HttpGet("{id}/result")]
    public ActionResult<BetResult> GetBetResult(int id) {
        var bet = _betService.GetBet(id);

        if(bet == null)
            return NotFound();

        return bet.BetResult;
    }
}
