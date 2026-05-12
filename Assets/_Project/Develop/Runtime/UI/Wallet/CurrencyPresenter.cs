using System;
using Develop.Runtime.Configs.Meta.Wallet;
using Develop.Runtime.UI.CommonViews;
using Develop.Runtime.Utilities.DataManagment;
using Develop.Runtime.Utilities.Reactive;

namespace Develop.Runtime.UI.Wallet
{
    public class CurrencyPresenter
    {
        //Бизнес логика
        private readonly IReadOnlyVariable<int> _currency;
        private readonly CurrencyTypes _currencyType;
        private readonly CurrencyIconsConfig _currencyIconsConfig;
        
        //Визуал
        private readonly IconTextView _view;

        private IDisposable _disposable;

        public CurrencyPresenter(
            IReadOnlyVariable<int> currency, 
            CurrencyTypes currencyType, 
            CurrencyIconsConfig currencyIconsConfig, 
            IconTextView view)
        {
            _currency = currency;
            _currencyType = currencyType;
            _currencyIconsConfig = currencyIconsConfig;
            _view = view;
        }

        public void Enable()
        {
            UpdateValue(_currency.Value);
            _view.SetIcon(_currencyIconsConfig.GetSpriteFor(_currencyType));

            _disposable = _currency.Subscribe(OnCurrencyChanged);
        }

        public void Disable()
        {
            _disposable.Dispose();
        }
        
        private void OnCurrencyChanged(int arg1, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => _view.SetText(value.ToString());
    }
}