//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/MakeNewWeapon", order = 0)]

//public class Weapon : ScriptableObject
//{
//    [SerializeField]
//    GameObject _source = null;

//    [SerializeField]
//    [Range(1.0f, 15.0f)]
//    float _range = 2.0f;
//    public float Range { get { return _range; } }

//    [SerializeField]
//    [Range(1.0f, 20.0f)]
//    float _damage = 5.0f;
//    public float Damage { get { return _damage; } }

//    [SerializeField]
//    AnimatorOverrideController _override = null;

//    [SerializeField]
//    bool _isRightHanded = true;

//    [SerializeField]
//    Projectile _projectile = null;
//    public bool HasProjectile { get { return _projectile != null; } }

//    TrailRenderer _trail = null;
//    public TrailRenderer Trail { get { return _trail; } }

//    const string _weaponName = "Weapon";

//    public void Spawn(Transform right, Transform left, Animator animator)
//    {
//        DestroyOldWeapon(right, left);

//        if (_source != null)
//        {
//            Transform hand = null;
//            hand = _isRightHanded ? right : left;
//            GameObject weapon = Instantiate(_source, hand);
//            weapon.name = _weaponName;

//            _trail = weapon.transform.GetComponentInChildren<TrailRenderer>();
//        }

//        if (_override)
//        {
//            animator.runtimeAnimatorController = _override;
//        }
//    }

//    public void LaunchProjectile(Transform right, Transform left, HealthPoint target)
//    {
//        Transform hand = null;
//        hand = _isRightHanded ? right : left;

//        Projectile projectile = Instantiate(_projectile, hand.position, Quaternion.identity);
//        projectile.SetTarget(target, _damage);
//    }

//    private void DestroyOldWeapon(Transform right, Transform left)
//    {
//        Transform old = right.Find(_weaponName);

//        if (old == null)
//        {
//            old = left.Find(_weaponName);
//        }

//        if (old == null) return;

//        Destroy(old.gameObject);
//    }
//}
