using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FROASTERY.Data;
using FROASTERY.Models;
using Microsoft.EntityFrameworkCore;

namespace FROASTERY.Services
{
    public class CartService
    {
        public event Action? OnChange;
        private int _cartItemCount;
                public int CartItemCount
        {
            get => _cartItemCount;
            private set
            {
                _cartItemCount = value;
                NotifyStateChanged();
        
            }
        }

        public void SetCount(int count)
        {
            CartItemCount = count;
            OnChange?.Invoke();
        }

        public void Increment() => SetCount(_cartItemCount + 1);
        public void Decrement() => SetCount(Math.Max(0, _cartItemCount - 1));

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}

