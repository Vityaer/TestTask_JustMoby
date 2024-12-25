using System.Collections.Generic;
using UI.Abstractions.Controllers;
using Zenject;

namespace UI.Abstractions.Windows
{
    public abstract class AbstractWindow : IInitializable, IWindow
    {
        private readonly List<IUiController> _controllers = new();

        public abstract void Initialize();

        public void Open()
        {
            foreach (var controller in _controllers)
            {
                if (controller.IsAutoShow)
                    controller.Show();
            }
        }

        public void Close()
        {
            foreach (var controller in _controllers)
            {
                controller.Hide();
            }
        }

        protected void AddController<T>(T controller)
            where T : IUiController
        {
            _controllers.Add(controller);
        }
    }
}
