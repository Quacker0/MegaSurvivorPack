using System.Collections.Generic;
using BepInEx;
using KinematicCharacterController;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;




namespace MegaSurvivorPack
{
  [BepInDependency(R2API.R2API.PluginGUID)]

  [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

  //why you reading this my code is shit

  public class SurvivorPlugin : BaseUnityPlugin
  {
    public const string PluginGUID = PluginAuthor + "." + PluginName;
    public const string PluginAuthor = "Quacker";
    public const string PluginName = "MegaSurvivorPack";
    public const string PluginVersion = "1.0.1";
    public const bool resize = false;
     

        public void Awake()
    {
      base.Logger.LogWarning("MegaSurvivorPack is loading...");

      
      On.RoR2.CharacterSelectBarController.Build += delegate(On.RoR2.CharacterSelectBarController.orig_Build orig, CharacterSelectBarController self)
			{
				self.iconContainerGrid.cellSize = new Vector2(64f, 64f);
				self.iconContainerGrid.constraintCount = 17;
				orig.Invoke(self);
        
			};
      base.Logger.LogWarning("MegaSurvivorPack : CharacterSelectBar eddited");

      
      var survivorlistdef = new List<survivorinfo>(){
        new survivorinfo() {name = "paladin", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "golem", dlcIdentifier = false, flyer = false, modelsize = 0.805f,CapY=-2.0f,camV= 1.4f},
        new survivorinfo() {name = "hand", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "beetleGuard", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "magmaWorm", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "electricWorm", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "impBoss", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "titangold", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "beetle", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "bison", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "megaDrone", dlcIdentifier = false, flyer = true},
        new survivorinfo() {name = "drone1", dlcIdentifier = false, flyer = true},
        //error new survivorinfo() {name = "flamedrone", dlcIdentifier = false, flyer = true},
        new survivorinfo() {name = "MissileDrone", dlcIdentifier = false, flyer = true},
        new survivorinfo() {name = "BackupDrone", dlcIdentifier = false, flyer = true},
        new survivorinfo() {name = "greaterWisp", dlcIdentifier = false, flyer = true},
        new survivorinfo() {name = "imp", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "lemurianBruiser", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "titan", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "bell", dlcIdentifier = false, flyer = true},
        new survivorinfo() {name = "clayBruiser", dlcIdentifier = false, flyer = false},
        //error new survivorinfo() {name = "roboBallBoss", dlcIdentifier = false, flyer = true},
        //error new survivorinfo() {name = "superRoboBallBoss", dlcIdentifier = false, flyer = true},
        new survivorinfo() {name = "vulture", dlcIdentifier = false, flyer = true},
        new survivorinfo() {name = "archWisp", dlcIdentifier = false, flyer = true},
        new survivorinfo() {name = "nullifier", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "scav", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "gravekeeper", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "parent", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "Heretic", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "minimushroom", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "Brother", dlcIdentifier = false, flyer = false,modelsize = 3.105f,CapY=-3.8f,camV= -7.0f},
        new survivorinfo() {name = "BrotherGlass", dlcIdentifier = false, flyer = false},
        new survivorinfo() {name = "BrotherHurt", dlcIdentifier = false, flyer = false},

      };

      addsurvivors(survivorlistdef);

      base.Logger.LogWarning("MegaSurvivorPack is loaded!");
    }

    public void addsurvivors(List<survivorinfo> survivorlist)
    {
      
      foreach (survivorinfo i in survivorlist)
      {
        
        GameObject bodycopy = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("prefabs/characterbodies/" + i.name + "Body"), i.name);
        //GameObject bodycopy2 = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("prefabs/characterbodies/" + i.name + "Body"), i.name);

        
        GameObject finalcopy = bodycopy.GetComponent<ModelLocator>().modelTransform.gameObject;

                
                    //finalcopy.transform.localScale -= new Vector3(i.modelsize, i.modelsize, i.modelsize);
                    //bodycopy.transform.localScale -= new Vector3(i.modelsize, i.modelsize, i.modelsize);

                    if (i.flyer)
                    {
                        //CharacterMotor flycontroler = bodycopy.GetComponent<CharacterMotor>();


                    }

                    if (!i.flyer)
                    {
                    if (resize)
                    {
                        KinematicCharacterMotor landcontroler = bodycopy.GetComponent<KinematicCharacterMotor>();



                        float OrgCapsuleRadius = landcontroler.CapsuleRadius;


                        float OrgCapsuleHeight = Reflection.GetFieldValue<float>(landcontroler, "CapsuleHeight");
                        float OrgCapsuleYOffset = Reflection.GetFieldValue<float>(landcontroler, "CapsuleYOffset");

                        //base.Logger.LogError("org radius | " + OrgCapsuleRadius.ToString());
                        //base.Logger.LogError("org height | " + OrgCapsuleHeight.ToString());
                        base.Logger.LogError("org r | " + i.name + " : " + OrgCapsuleRadius.ToString());
                        base.Logger.LogError("org h | " + i.name + " : " + OrgCapsuleHeight.ToString());
                        base.Logger.LogError("org y | " + i.name + " : " + OrgCapsuleYOffset.ToString());

                        Reflection.SetFieldValue<float>(landcontroler, "CapsuleRadius", OrgCapsuleRadius = 0.74f);
                        Reflection.SetFieldValue<float>(landcontroler, "CapsuleHeight", OrgCapsuleHeight = 2.14f);
                        Reflection.SetFieldValue<float>(landcontroler, "CapsuleYOffset", OrgCapsuleYOffset = i.CapY);

                        float NewCapsuleRadius = Reflection.GetFieldValue<float>(landcontroler, "CapsuleRadius");
                        float NewCapsuleHeight = Reflection.GetFieldValue<float>(landcontroler, "CapsuleHeight");
                        float NewCapsuleYOffset = Reflection.GetFieldValue<float>(landcontroler, "CapsuleYOffset");

                        //base.Logger.LogError("new radius  | " + NewCapsuleRadius.ToString());
                        //base.Logger.LogError("new height  | " + NewCapsuleHeight.ToString());
                        base.Logger.LogError("new y  | " + i.name + " : " + NewCapsuleRadius.ToString());
                        base.Logger.LogError("new y  | " + i.name + " : " + NewCapsuleHeight.ToString());
                        base.Logger.LogError("new y  | " + i.name + " : " + NewCapsuleYOffset.ToString());




                        //camerapram.RemoveParamsOverride(handle);







                        //CameraTargetParams camerapram = gameObject.GetComponent<CameraTargetParams>();
                        //camerapram.cameraParams.pivotVerticalOffset -= 0.81f;

                    }
                    };
                

                
                Interactor interact = bodycopy.GetComponent<Interactor>();
                interact.maxInteractionDistance = 6f;


                CameraTargetParams camerapram = bodycopy.GetComponent<CameraTargetParams>();

                camerapram.cameraParams = ScriptableObject.CreateInstance<CharacterCameraParams>();
                var copy = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").GetComponent<CameraTargetParams>().cameraParams;


                camerapram.cameraParams.data.pivotVerticalOffset = 0;//i.camV;
                camerapram.cameraParams.name = "quackcam " + i.name;
                camerapram.cameraParams.data.maxPitch = copy.data.maxPitch;
                camerapram.cameraParams.data.minPitch = copy.data.minPitch;
                camerapram.cameraParams.data.wallCushion = copy.data.wallCushion;
                camerapram.cameraParams.data.idealLocalCameraPos = new Vector3(0f, i.camV, -10f);

                camerapram.cameraPivotTransform = null;
            
                camerapram.recoil = Vector2.zero;
                
                camerapram.dontRaycastToPivot = false;

                



                SurvivorDef newsurvivor = ScriptableObject.CreateInstance<SurvivorDef>();
				newsurvivor.bodyPrefab = bodycopy;
				newsurvivor.bodyPrefab.GetComponent<CharacterBody>().preferredPodPrefab = Resources.Load<GameObject>("Prefabs/NetworkedObjects/SurvivorPod");
				newsurvivor.descriptionToken = i.name + ": MegaSurvivorPack \n";
				newsurvivor.displayPrefab = finalcopy;
                newsurvivor.cachedName = i.name;
        

				newsurvivor.primaryColor = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
				base.Logger.LogMessage(i.name + " added ");
				
                R2API.ContentAddition.AddBody(newsurvivor.bodyPrefab);
				R2API.ContentAddition.AddSurvivorDef(newsurvivor);

        
      }
      
    
      

    }

    
  }

  
public class survivorinfo
{
  public string name = "quack";
  public bool dlcIdentifier = false;
  public bool flyer = false;

  public float modelsize = 0f;
  public float CapR = 0f;
  public float CapH = 0f;
  public float CapY = 0f;
  public float camV = 0f;
}
  
}
