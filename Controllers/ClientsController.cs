using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notifier.Data.Entities;
using Notifier.Dtos.Client;
using Notifier.Helpers;
using Notifier.Services;

namespace Notifier.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly ISubscriptionTextService _textService;
        private readonly IMapper _mapper;

        public ClientsController(IClientService clientService, ISubscriptionService subscriptionService, ISubscriptionTextService textService, IMapper mapper)
        {
            _clientService = clientService;
            _subscriptionService = subscriptionService;
            _textService = textService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var clients = (await _clientService.GetAll())
                .Select(x => _mapper.Map<Client, GetClientDto>(x));
            return View(clients);
        }

        public async Task<IActionResult> Store([FromForm]CreateClientDto clientDto, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateClientDto, Client>(clientDto);
            await _clientService.Save(entity, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult CreateMany()
        {
            return View();
        }

        public async Task<IActionResult> StoreMany([FromForm] CreateManyClientsDto clientsDto, CancellationToken cancellationToken)
        {
            var clients = clientsDto.ParseClients();
            await _clientService.SaveMany(clients, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}