namespace Ecommerce.Web.Controllers
{
    public class ArticleController : _BaseController
    {
        private readonly Context context;
        private readonly IStringLocalizer<ArticleController> localizer;

        public ArticleController(Context context, IStringLocalizer<ArticleController> localizer, IOptions<RequestLocalizationOptions> locOptions, IMemoryCache memoryCache, MailService mailService)
        {
            this.context = context;
            this.localizer = localizer;
        }



        public IActionResult Index(int lotId)
        {
            var lot = context.Lots.FirstOrDefault(l => l.LotId == lotId);

            if (lot != null)
            {
                return View(new IndexPublicViewModel()
                {
                    Description = lot.Description,
                    LotId = lot.LotId,
                    State = lot.State
                });
            }
            else
                return RedirectToAction("Status", "Error", new { code = 404 });
        }


        public IActionResult IndexPublic(int lotId)
        {

            var lot = context.Lots.FirstOrDefault(l => l.LotId == lotId && l.State == Lot.LotState.ENABLED);
            if (lot != null)
            {
                var takenId = context.Lots.FirstOrDefault(l => l.LotId == lotId).Articles.FirstOrDefault(a => a.UserArticles.Any(x => x.UserId == CurrentUserId)) == null ? 0
                : context.Lots.FirstOrDefault(l => l.LotId == lotId).Articles.FirstOrDefault(a => a.UserArticles.Any(x => x.UserId == CurrentUserId)).ArticleId;

                IndexPublicViewModel indexPublicViewModel = new IndexPublicViewModel
                {
                    Description = lot.Description,
                    TakenId = takenId,
                    LotId = lotId
                };

                return View(indexPublicViewModel);
            }

            return RedirectToAction("Status", "Error", new { code = 404 });
        }

        public JsonResult GetArticles(int lotId)
        {
            var article = context.Articles.Where(a => a.Lot.LotId == lotId).Select(l => new
            {
                article_Description = l.Description,
                state = localizer[l.State.GetAttribute<DisplayAttribute>().Name].Value,
                serialNumber = l.SerialNumber,
                type = l.Type.Name,
                article_id = l.ArticleId,
                brand = l.Brand,
                price = "$\n" + l.Price.ToString(),
                adjudicated = l.AdjudicatedUser.Username.ToString() == null ? "Sin usuario" : l.AdjudicatedUser.Username.ToString(),
                userCount = l.UserArticles.Count
            }).ToList();

            return Json(article);
        }

        public JsonResult GetArticlesPublic(int lotId)
        {
            var articleWon = context.Articles.FirstOrDefault(x =>
            x.AdjudicatedUserId == CurrentUserId &&
            x.Lot.UpdateDate.Year == DateTime.Now.Year);

            List<Article> articleList = context.Articles.Where(a => a.Lot.LotId == lotId && a.State == Article.ArticleState.ENABLED).ToList();

            if (articleWon != null)
                articleList = articleList.Where(x => x.TypeId != articleWon.TypeId).ToList();

            var articles = articleList.Select(l => new
            {
                article_Description = l.Description,
                serialNumber = l.SerialNumber,
                type = l.Type.Name,
                article_id = l.ArticleId,
                brand = l.Brand,
                userCount = l.UserArticles.Count,
                price = "$\n" + l.Price.ToString()
            }).ToList();

            return Json(articles);
        }



        [HttpGet]
        public IActionResult CreateArticle(int lotId, int articleId)
        {
            if (context.Lots.Any(x => x.LotId == lotId))
            {
                var model = new CreateArticleViewModel();

                model.LotId = lotId;
                var typeList = new List<SelectListItem>();

                foreach (var item in context.ArticleTypes)
                {
                    typeList.Add(new SelectListItem { Text = item.Name, Value = item.ArticleTypeId.ToString() });
                }
                model.Types = typeList;

                if (articleId != 0)
                {
                    var article = context.Articles.FirstOrDefault(a => a.ArticleId == articleId);

                    if (article != null)
                    {
                        model.Price = article.Price;
                        model.SerialNumber = article.SerialNumber;
                        model.Description = article.Description;
                        model.Brand = article.Brand;
                        model.TypeId = article.TypeId;
                    }
                    else
                    {
                        return RedirectToAction("Index", new { lotId = lotId });
                    }
                }

                return View(model);
            }
            else
                return RedirectToAction("Index", new { lotId = lotId });
        }

        [HttpPost]
        public IActionResult CreateArticle(CreateArticleViewModel createArticleViewModel)
        {
            if (ModelState.IsValid)
            {
                var article = new Article
                {
                    TypeId = createArticleViewModel.TypeId,
                    Brand = createArticleViewModel.Brand,
                    SerialNumber = createArticleViewModel.SerialNumber,
                    State = Article.ArticleState.ENABLED,
                    LotId = createArticleViewModel.LotId,
                    Description = createArticleViewModel.Description,
                    UserArticles = new List<UserArticle>(),
                    Price = (int)createArticleViewModel.Price,
                };

                context.Articles.Add(article);
                context.SaveChanges();

                return RedirectToAction("Index", new { lotId = article.LotId });
            }
            else
            {

                var typeList = new List<SelectListItem>();

                foreach (var item in context.ArticleTypes)
                {
                    typeList.Add(new SelectListItem { Text = item.Name, Value = item.ArticleTypeId.ToString() });
                }
                createArticleViewModel.Types = typeList;

                return View(createArticleViewModel);
            }
        }


        [HttpGet]
        public IActionResult EditArticle(int articleId, int lotId)
        {

            if (context.Lots.Any(x => x.LotId == lotId))
            {
                var article = context.Articles.FirstOrDefault(u => u.ArticleId == articleId);

                if (article == null)
                    return RedirectToAction("Index", new { lotId = lotId });
                else
                {
                    var model = new EditArticleViewModel
                    {
                        TypeId = article.TypeId,
                        Brand = article.Brand,
                        Description = article.Description,
                        SerialNumber = article.SerialNumber,
                        State = article.State,
                        Lot_ID = article.Lot.LotId,
                        ArticleId = articleId,
                        Price = article.Price
                    };
                    var typeList = new List<SelectListItem>();

                    foreach (var item in context.ArticleTypes)
                    {
                        typeList.Add(new SelectListItem { Text = item.Name, Value = item.ArticleTypeId.ToString() });
                    }
                    model.Types = typeList;

                    return View(model);
                }

            }
            else
                return RedirectToAction("Index", new { lotId = lotId });
        }

        [HttpPost]
        public IActionResult EditArticle(EditArticleViewModel editarArticleViewModel)
        {
            var article = context.Articles.FirstOrDefault(u => u.ArticleId == editarArticleViewModel.ArticleId);

            if (ModelState.IsValid)
            {
                if (article != null)
                {
                    article.TypeId = editarArticleViewModel.TypeId;
                    article.Brand = editarArticleViewModel.Brand;
                    article.Description = editarArticleViewModel.Description;
                    article.SerialNumber = editarArticleViewModel.SerialNumber;
                    article.Price = (int)editarArticleViewModel.Price;
                    context.SaveChanges();
                }
                return RedirectToAction("Index", new { lotId = article.Lot.LotId });
            }
            else
            {
                var typeList = new List<SelectListItem>();

                foreach (var item in context.ArticleTypes)
                {
                    typeList.Add(new SelectListItem { Text = item.Name, Value = item.ArticleTypeId.ToString() });
                }
                editarArticleViewModel.Types = typeList;

                return View(editarArticleViewModel);
            }
        }
    }
}
