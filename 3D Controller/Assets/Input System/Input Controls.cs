//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input System/Input Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""b38dd956-52e8-4dd0-a725-423485b0acc4"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f1aa66ae-a31e-40ec-b548-f4bea6d10519"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""db0bae9b-3b08-4ebe-a22a-e499363aeaf1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""61094c90-8aee-4be2-8169-dd1e927e2f22"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""c78c9034-29d6-43fc-b257-88093af72042"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Activate Camera"",
                    ""type"": ""Button"",
                    ""id"": ""cd0cb8d7-a0ed-411b-8b71-53c1f95dabb4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""f0281288-8402-45c4-9e0f-be0e71269496"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""6a6999a4-d341-491e-b9c5-14fd3e9e3b9b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Lock On Camera"",
                    ""type"": ""Button"",
                    ""id"": ""459518aa-83e5-4581-8b79-9d3b99544ce9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Cast Spell 1"",
                    ""type"": ""Button"",
                    ""id"": ""94aef330-a31f-45ef-b9b3-e25afc2d8504"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""Scale(factor=0)"",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cast Spell 2"",
                    ""type"": ""Button"",
                    ""id"": ""345e4395-c20d-4988-b798-b8d11723824a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""Scale"",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cast Spell 3"",
                    ""type"": ""Button"",
                    ""id"": ""0d0c0fff-44e8-48e3-b049-3c6560c764ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""Scale(factor=2)"",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cast Spell 4"",
                    ""type"": ""Button"",
                    ""id"": ""c57a460a-c4f2-4c7d-886e-833fa306e06e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""Scale(factor=3)"",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchTarget"",
                    ""type"": ""Value"",
                    ""id"": ""27c5e435-9226-4a13-8566-0fd7e7aa4e4b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d890c59f-fd63-4297-a7ec-5cfb72d2561b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""3f08c380-3726-498e-b585-052c4fcb99be"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""c730fb1f-e8bc-4754-94d4-53601887e354"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""5e5c18c7-bd2d-465a-8aba-25b2ba56856b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""5e90cad8-7b87-4a11-8245-3b0affb39b4c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""871f2079-e2e0-4300-af7c-7dd3d6bdc27d"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32923425-b51d-42cc-a110-2b3b264ac643"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff4bfc86-d311-4325-a54e-6a4166326be9"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1ed3f68-4985-4912-a0c5-03aa5bde13f1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Activate Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c352c404-ce83-4793-a210-a2478091bfba"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a36149e-2a6e-4e4d-9436-65df9264ec8d"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e914c322-0d4d-4e26-9ded-2b335bf205c5"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a935720-6780-42e0-9ad3-a77fbd8a7f78"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cast Spell 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4f0dea3-29fd-4aba-9200-2cec35a00634"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cast Spell 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ff72ec8-6005-4574-9626-443584a8bbd2"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cast Spell 3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fa9e5ef-c357-4d84-a0bb-f4ec1578a53d"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cast Spell 4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""6b5333a9-d706-4f45-be65-999f6daa7458"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchTarget"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5249df85-5e68-4df5-945c-fd92b7637563"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2ea98eff-498c-4e16-bcfd-9a44ae4e311a"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
        m_Player_ActivateCamera = m_Player.FindAction("Activate Camera", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_Block = m_Player.FindAction("Block", throwIfNotFound: true);
        m_Player_LockOnCamera = m_Player.FindAction("Lock On Camera", throwIfNotFound: true);
        m_Player_CastSpell1 = m_Player.FindAction("Cast Spell 1", throwIfNotFound: true);
        m_Player_CastSpell2 = m_Player.FindAction("Cast Spell 2", throwIfNotFound: true);
        m_Player_CastSpell3 = m_Player.FindAction("Cast Spell 3", throwIfNotFound: true);
        m_Player_CastSpell4 = m_Player.FindAction("Cast Spell 4", throwIfNotFound: true);
        m_Player_SwitchTarget = m_Player.FindAction("SwitchTarget", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Run;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Dash;
    private readonly InputAction m_Player_ActivateCamera;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_Block;
    private readonly InputAction m_Player_LockOnCamera;
    private readonly InputAction m_Player_CastSpell1;
    private readonly InputAction m_Player_CastSpell2;
    private readonly InputAction m_Player_CastSpell3;
    private readonly InputAction m_Player_CastSpell4;
    private readonly InputAction m_Player_SwitchTarget;
    public struct PlayerActions
    {
        private @InputControls m_Wrapper;
        public PlayerActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputAction @ActivateCamera => m_Wrapper.m_Player_ActivateCamera;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @Block => m_Wrapper.m_Player_Block;
        public InputAction @LockOnCamera => m_Wrapper.m_Player_LockOnCamera;
        public InputAction @CastSpell1 => m_Wrapper.m_Player_CastSpell1;
        public InputAction @CastSpell2 => m_Wrapper.m_Player_CastSpell2;
        public InputAction @CastSpell3 => m_Wrapper.m_Player_CastSpell3;
        public InputAction @CastSpell4 => m_Wrapper.m_Player_CastSpell4;
        public InputAction @SwitchTarget => m_Wrapper.m_Player_SwitchTarget;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Dash.started += instance.OnDash;
            @Dash.performed += instance.OnDash;
            @Dash.canceled += instance.OnDash;
            @ActivateCamera.started += instance.OnActivateCamera;
            @ActivateCamera.performed += instance.OnActivateCamera;
            @ActivateCamera.canceled += instance.OnActivateCamera;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
            @Block.started += instance.OnBlock;
            @Block.performed += instance.OnBlock;
            @Block.canceled += instance.OnBlock;
            @LockOnCamera.started += instance.OnLockOnCamera;
            @LockOnCamera.performed += instance.OnLockOnCamera;
            @LockOnCamera.canceled += instance.OnLockOnCamera;
            @CastSpell1.started += instance.OnCastSpell1;
            @CastSpell1.performed += instance.OnCastSpell1;
            @CastSpell1.canceled += instance.OnCastSpell1;
            @CastSpell2.started += instance.OnCastSpell2;
            @CastSpell2.performed += instance.OnCastSpell2;
            @CastSpell2.canceled += instance.OnCastSpell2;
            @CastSpell3.started += instance.OnCastSpell3;
            @CastSpell3.performed += instance.OnCastSpell3;
            @CastSpell3.canceled += instance.OnCastSpell3;
            @CastSpell4.started += instance.OnCastSpell4;
            @CastSpell4.performed += instance.OnCastSpell4;
            @CastSpell4.canceled += instance.OnCastSpell4;
            @SwitchTarget.started += instance.OnSwitchTarget;
            @SwitchTarget.performed += instance.OnSwitchTarget;
            @SwitchTarget.canceled += instance.OnSwitchTarget;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Dash.started -= instance.OnDash;
            @Dash.performed -= instance.OnDash;
            @Dash.canceled -= instance.OnDash;
            @ActivateCamera.started -= instance.OnActivateCamera;
            @ActivateCamera.performed -= instance.OnActivateCamera;
            @ActivateCamera.canceled -= instance.OnActivateCamera;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
            @Block.started -= instance.OnBlock;
            @Block.performed -= instance.OnBlock;
            @Block.canceled -= instance.OnBlock;
            @LockOnCamera.started -= instance.OnLockOnCamera;
            @LockOnCamera.performed -= instance.OnLockOnCamera;
            @LockOnCamera.canceled -= instance.OnLockOnCamera;
            @CastSpell1.started -= instance.OnCastSpell1;
            @CastSpell1.performed -= instance.OnCastSpell1;
            @CastSpell1.canceled -= instance.OnCastSpell1;
            @CastSpell2.started -= instance.OnCastSpell2;
            @CastSpell2.performed -= instance.OnCastSpell2;
            @CastSpell2.canceled -= instance.OnCastSpell2;
            @CastSpell3.started -= instance.OnCastSpell3;
            @CastSpell3.performed -= instance.OnCastSpell3;
            @CastSpell3.canceled -= instance.OnCastSpell3;
            @CastSpell4.started -= instance.OnCastSpell4;
            @CastSpell4.performed -= instance.OnCastSpell4;
            @CastSpell4.canceled -= instance.OnCastSpell4;
            @SwitchTarget.started -= instance.OnSwitchTarget;
            @SwitchTarget.performed -= instance.OnSwitchTarget;
            @SwitchTarget.canceled -= instance.OnSwitchTarget;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnActivateCamera(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnLockOnCamera(InputAction.CallbackContext context);
        void OnCastSpell1(InputAction.CallbackContext context);
        void OnCastSpell2(InputAction.CallbackContext context);
        void OnCastSpell3(InputAction.CallbackContext context);
        void OnCastSpell4(InputAction.CallbackContext context);
        void OnSwitchTarget(InputAction.CallbackContext context);
    }
}
