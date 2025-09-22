public class GoStraight : Command
{
    private PlayerController _controller;

    public GoStraight(PlayerController controller)
    {
        _controller = controller;
    }

    public override void Execute()
    {
        _controller.Turn(PlayerController.Direction.Straight);
    }
}
