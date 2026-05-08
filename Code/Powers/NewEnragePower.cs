using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
namespace TestSubjectEnrageBalance.Code.Powers;

public sealed class NewEnragePower: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
	
	public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
	{
		if (cardPlay.Card.Type == CardType.Skill)
		{
			await Cmd.Wait(0.5f);
			await PowerCmd.Apply<EnragedPower>(context, cardPlay.Card.Owner.Creature, base.Amount, base.Owner, null);
		}
	}
}
