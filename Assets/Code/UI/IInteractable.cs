using Code.UI.Selection;

namespace Code.UI
{
	public interface IInteractable
	{
		bool Interact(ISelectable selectedObject);
	}
}