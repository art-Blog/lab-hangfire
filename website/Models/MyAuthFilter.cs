using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace website.Models;

public class MyAuthFilter : IDashboardAuthorizationFilter {
    public bool Authorize([NotNull] DashboardContext context) {
        // 後面可以改別的邏輯，例如判斷session是否存在，或是identity claim等等
        // 此處測試直接讓任何人都可以瀏覽
        return true;
    }
}