using Microsoft.EntityFrameworkCore;
using Review.Comments.Entities;

namespace Review.Comments.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Comments.Any())
                {
                    return;   //Data was already seeded
                }

                context.Comments.AddRange(
                    new Comment()
                    {
                        Id = 1,
                        UserId = 1,
                        ProductId = 1,
                        Description = "Poured from a bottle into a chalice glass Appearance – The brew pours a lightly hazed yellow amber color with a puffy head of white foam. The head has a fantastic level of retention, with it very slowly fading over time to leave tons of foamy lace on the sides of the glass. Smell – The aroma of the brew is very heavy of a candied sugar and sweet peach and apricot smell mixed with some aromas of a biscuit and cracker malt. At the same time there is a rather decent showing of a yeastcake and a great deal of clove and coriander. Other aromas of a black and white peppercorn and a little bit of a hay are there as well, producing a rather inviting aroma overall. Taste – The taste starts out with a nice biscuit and cracker malt flavor paired with the apricot and peach that was in the nose. At the same time the candied sugar tastes and some flavors of white grape are there as well, giving a nice, but not overwhelming sweetness. Clove and coriander spice are there as well, both of which grow slightly stronger as the taste moves forward. Some yeast and lemongrass as well as a touch of pepper arrive on the scene all while the sweet dwindles a little bit. With a touch of grain and a bit of straw coming at the very end, one is left with a very pleasant, moderately sweet, but well-balanced triple taste to linger on the tongue. Mouthfeel – The body of the brew is light and crisp with a carbonation level that is high with tons of effervescent bubbles. For the style and the tastes, the feel is spot on and makes for one very nice sipper. Overall – A wonderful triple with lots of flavor and a sweeter, but very well-balanced taste. Fantastic!",
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Comment()
                    {
                        Id = 2,
                        UserId = 2,
                        ProductId = 2,
                        Description = "I could have sworn I had this beer before, just because I’ve had a ton of Prairie. Like most of their stouts, this is insanely good. Standard appearance. Sweet notes of vanilla and chocolate play well with the barrel presence on the nose. The sweet adjuncts take the forefront on taste, but there’s an underlying flavor of rum barrel throughout each sip. The best thing about Prairie barrel aged beers is they have a mild feel-they’re not thick of syrupy, and the alcohol presents itself with a burn, but not an overpowering one. A total sweet dessert beer, yet another winner from them.",
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Comment()
                    {
                        Id = 3,
                        UserId = 3,
                        ProductId = 2,
                        Description = "16oz can from Half Time. Hazy yellow straw body, small frothy head. Hoppy, dank aroma, oranges, overripe melon. Taste is orange juice, dank, grapefruit peel. Taste fades, but bitterness lingers",
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Comment()
                    {
                        Id = 4,
                        UserId = 2,
                        ProductId = 3,
                        Description = "Pours a very dark copper color. clear. thick cream colored head. great head retention. very nice aroma - toasty caramel malt, classic C-hops - citrus, dank pine. similar flavors in the taste, good balance between the malt and firm bitterness. just a touch boozy. sticky mouthfeel, medium bodied.",
                        IsActive = true,
                        IsDeleted = true,
                        CreatedAt = DateTime.Now,
                    });

                context.SaveChanges();
            }
        }
    }
}
