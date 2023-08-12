using System.Diagnostics;
using Final_Project.Data;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _accessor;
    //private readonly ISliderService _sliderService;
    //private readonly ICategoryService _categoryService;
    //private readonly ISubcategoryService _subcategoryService;
    //private readonly IProductService _productService;
    //private readonly IAdvertisingService _advertisingService;
    //private readonly IBrandService _brandService;
    //private readonly IBlogService _blogService;
    //private readonly IBasketService _basketService;

    public HomeController(AppDbContext context,
                          IHttpContextAccessor accessor
                          //ICategoryService categoryService,
                          //ISubcategoryService subcategoryService,
                          //IProductService productService,
                          //IAdvertisingService advertisingService,
                          //IBrandService brandService,
                          //IBlogService blogService,
                          //ISliderService sliderService,
                          //IBasketService basketService,
        )
    {
        _context = context;
        _accessor = accessor;
        //_sliderService = sliderService;
        //_categoryService = categoryService;
        //_subcategoryService = subcategoryService;
        //_productService = productService;
        //_advertisingService = advertisingService;
        //_brandService = brandService;
        //_blogService = blogService;
        //_basketService = basketService;
    }

    public IActionResult Index()
    {



        return View();
    }
}

