namespace AttackOnTap.Characters
{
    public interface IPlayableCharacter
    {
        void BasicAttack();
        void RangedAttack();
        void SpecialAttack();

        void Celebrate(int type);
    }
}
