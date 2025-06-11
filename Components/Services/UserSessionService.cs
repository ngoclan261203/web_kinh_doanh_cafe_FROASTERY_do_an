using System;

namespace FROASTERY.Services
{
    public class UserSessionService
    {
        public bool IsLoggedIn { get; private set; }
        public string? TenNguoiDung { get; private set; }

        public event Action? OnChange;

        public void Login(string tenNguoiDung)
        {
            IsLoggedIn = true;
            TenNguoiDung = tenNguoiDung;

            Console.WriteLine($"[UserSessionService] Login() được gọi. Tên: {tenNguoiDung}, IsLoggedIn: {IsLoggedIn}");
            
            NotifyStateChanged();
        }

        public void Logout()
        {
            IsLoggedIn = false;
            TenNguoiDung = null;

            Console.WriteLine("[UserSessionService] Logout() được gọi. IsLoggedIn đặt lại false.");

            NotifyStateChanged();
        }

        private void NotifyStateChanged()
        {
            Console.WriteLine("[UserSessionService] NotifyStateChanged() được gọi.");
            OnChange?.Invoke();
        }
    }
}
