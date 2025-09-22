public class TurnLeft : Command
{
    private PlayerController _controller;

    public TurnLeft(PlayerController controller)
    {
        _controller = controller;
    }

    public override void Execute()
    {
        _controller.Turn(PlayerController.Direction.Left);
    }
}
