using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notifier.Data.Entities;
using Notifier.Dtos.SubscriptionText;
using Notifier.Services;

namespace Notifier.Controllers
{
    public class SubscriptionTextsController : Controller
    {
        private readonly ISubscriptionTextService _subscriptionTextService;
        private readonly IMapper _mapper;
        public SubscriptionTextsController(ISubscriptionTextService subscriptionTextService, IMapper mapper)
        {
            _subscriptionTextService = subscriptionTextService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var output = (await _subscriptionTextService.Get())
                .Select(x => _mapper.Map<SubscriptionText, GetSubscriptionTextDto>(x));
            return View(output);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Store([FromForm] CreateSubscriptionTextDto subscriptionText)
        {
            var entity = _mapper.Map<CreateSubscriptionTextDto, SubscriptionText>(subscriptionText);
            await _subscriptionTextService.Save(entity);
            return RedirectToAction("Index");
        }
    }
}