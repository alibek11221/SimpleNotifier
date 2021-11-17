using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notifier.Data.Entities;
using Notifier.Dtos.Client;
using Notifier.Dtos.Subscription;
using Notifier.Services;

namespace Notifier.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ISubscriptionTextService _subscriptionTextService;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        public SubscriptionsController(ISubscriptionService subscriptionService, ISubscriptionTextService subscriptionTextService, IClientService clientService, IMapper mapper)
        {
            _subscriptionService = subscriptionService;
            _subscriptionTextService = subscriptionTextService;
            _clientService = clientService;
            _mapper = mapper;

        }
        // GET
        public async Task<IActionResult> Index()
        {
            var output = await _subscriptionService.GetAll();
            var subscriptions = output
                .Select(x => _mapper.Map<Subscription, GetSubscriptionDto>(x));
            return View(subscriptions);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> SubscribeClients()
        {
            ViewBag.Subscriptions = await _subscriptionService.GetAll();
            ViewBag.Clients = await _clientService.GetAll();
            return View();
        }
        [HttpPost] 
        public async Task<IActionResult> SubscribeClients([FromForm] SubscribeClientsDto clientsDto, CancellationToken cancellationToken)
        {
            await _subscriptionService.SubscribeMany(clientsDto.SubscriptionId, clientsDto.ClientIds, cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Store([FromForm] CreateSubscriptionDto subscriptionText)
        {
            var entity = _mapper.Map<CreateSubscriptionDto, Subscription>(subscriptionText);
            await _subscriptionService.Save(entity);
            await _subscriptionTextService.AddToSubscription(subscriptionText.TextId, entity);
            return RedirectToAction(nameof(Index));
        }
    }
}