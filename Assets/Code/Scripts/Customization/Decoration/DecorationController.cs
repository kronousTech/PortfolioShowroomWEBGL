using System.Collections.Generic;

namespace KronosTech.Customization.Decoration
{
    public static class DecorationController
    {
        private static bool _currentState = true;
        private static readonly List<DecorationModel> _modelsList = new();

        public static void SetVisibility(bool state)
        {
            foreach (var model in _modelsList)
            {
                model.gameObject.SetActive(state);
            }

            _currentState = state;
        }

        public static void Add(DecorationModel model)
        {
            _modelsList.Add(model);

            model.gameObject.SetActive(_currentState);
        }
    }
}