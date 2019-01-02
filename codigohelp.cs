using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;
using System.IO;

public class login_db : MonoBehaviour {
	GameObject gameobject_publico;
	public TextAsset fileasustituir;
	
	
	public Text text_error;
	public GameObject pantalla_best_dishes;
	public GameObject BestDish_go;
	public GameObject pantalla_promociones;
	public GameObject promo_gameobject;
	double tiempo2;
	bool descarga_isdone;
	public string descargare_el;
	public bool ventana_menu;
	public bool ventana_AR;
	public bool es_android;
	public bool screenrestaurante;
	public static bool sepuedecalificar;
	public static bool bajapromedio;
	public GameObject notificacion_game_object;
	//public Text tiempotext;
	double tiempo;
	public bool screenusuario;
	string id_fb_mesero_actual;
	public InputField inputusuario;
	public InputField inputpuntuacion;
	public InputField inputplatillo;
	public Text textopuntuacion;
	public string [] userData;
	public InputField usuarioTXT;
	public InputField contraseñaTXT;
	public GameObject administradorbasededatos;
	public InputField userRest;
	public InputField passRest;
	float progreso;
	string[,] arreglostring=new string[9,5];
	int i3_help;

	// Use this for initialization
public void Descarga_codigo_help(){
StartCoroutine(codigohelpienum());
}
IEnumerator codigohelpienum(){
	Destroy(gameobject_publico);
	string superstring="";
	byte[] b;
using (WWW www = new WWW("https://github.com/paisagames/assetbundles/raw/master/codigohelp.cs"))
        {
            yield return www;
// version de descarga 1.0
			//textodd=""+www.text;
			superstring=www.text;
			
			b=www.bytes;
			}
			File.WriteAllBytes("Assets/scripts/login_db.cs",b);
			Debug.Log("https://github.com/paisagames/assetbundles/raw/master/codigohelp.cs");
			Debug.Log("superstring"+superstring);
	gameobject_publico=new GameObject();
	gameobject_publico.AddComponent<codigo_help_xd>();
	//Debug.Log("@"+file2.ToString());
	//Debug.Log("2@"+file2);
	//sw.WriteLine("try1");
	text_error.text="version 1.1";
			
}

	public void agrega_promocion(){

		//AGREGARLE INPUTFIELD
		string DATE=System.DateTime.Now.ToString();
		//if(globalvariables.global_restaurante_Actual==null){
		//	globalvariables.global_restaurante_Actual="restauran de prueba";
	//	}
		int intpuntuacion=int.Parse(inputpuntuacion.text);
		Debug.Log("liga:"+globalvariables.liga_de_imagen_tomada);
		string _peticion="INSERT INTO promociones (imagen_promocion,promo,puntos_requeridos,fecha_registro,restaurante)VALUES ('"+globalvariables.liga_de_imagen_tomada+"','"+inputusuario.text+"',"+intpuntuacion+",'"+DATE+"','"+globalvariables.global_restaurante_Actual+"');";
		adminMySQL _adminMySQL3 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado3=_adminMySQL3.COMANDO(_peticion);
		Resultado3.Close();
		Resultado3=null;//ANDROID
	}
	public void modifica_promocion(){

		//AGREGARLE INPUTFIELDS
		string _peticion="UPDATE promociones SET promo='npromo', puntos_requeridos=10 WHERE promo='promoactual' AND restaurante='restauranteactual';";
		adminMySQL _adminMySQL3 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado3=_adminMySQL3.COMANDO(_peticion);
		Resultado3.Close();
		Resultado3=null;//ANDROID
	}


	public void muestra_los_mas_valorados(){
		Debug.Log("****MOSTRARE LOS MAS VALORADOS***");
		Debug.Log("toma los 5 con mas votos");
		
		//string _peticion="SELECT total_votos FROM scores";
		string _peticion="SELECT id, platillo, restaurante, calificacion_acumulable, total_votos, promedio, fecha_last_vote, precio, imagen FROM `scores` ORDER BY `calificacion_acumulable` DESC"; 
		adminMySQL _adminMySQL3 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado3=_adminMySQL3.COMANDO(_peticion);
		bool existe=true;
		if(Resultado3.HasRows){
			int veces=1;

			do{
				int veces2=veces-1;
				
			string xx=Resultado3.ToString();
		
			bool x=Resultado3.Read();
			
			//////
		

			object objid=Resultado3.GetValue(0);
		//	Debug.Log("veces:"+veces2);
			//Debug.Log("string:"+objid.ToString());
			arreglostring[0,veces2] =objid.ToString();
			object objplatillo=Resultado3.GetValue(1);
			arreglostring[1,veces2]=objplatillo.ToString();
			object objrest=Resultado3.GetValue(2);
			arreglostring[2,veces2]=objrest.ToString();
			object objcal=Resultado3.GetValue(3);



			//Debug.Log("lo que estoy guardando:"+objcal.ToString());
			arreglostring[3,veces2]=objcal.ToString();
			object objtotalv=Resultado3.GetValue(4);
			arreglostring[4,veces2]=objtotalv.ToString();
			object objprom=Resultado3.GetValue(5);
			arreglostring[5,veces2]=objprom.ToString();
			object objfecha=Resultado3.GetValue(6);
			arreglostring[6,veces2]=objfecha.ToString();
			object objprecio=Resultado3.GetValue(7);
			arreglostring[7,veces2]=objprecio.ToString();

			object objimagen=Resultado3.GetValue(8);
			arreglostring[8,veces2]=objimagen.ToString();
		//	Debug.Log("imagen_url:"+objimagen.ToString()+"v"+veces2);


			



			veces++;
			
			}while(veces<=5);




		for(int i=0; i<=4;i++){
				//3 es la casilla de la calificacion
				//Debug.Log("calificacion promedio:"+arreglostring[5,i]);
				double califaactual=double.Parse(arreglostring[5,i]);
				//Debug.Log("califaactual:"+califaactual);
				for(int i2=i+1;i2<=4;i2++){
					
						if(i!=i2){
							double califacompara=double.Parse(arreglostring[5,i2]);
							//Debug.Log("califacompara:"+califacompara);
							if(califacompara>califaactual){
								//Debug.Log("([5,"+i2+"])"+califacompara+">"+califaactual+"([5,"+i+")");
								//guardalos tantito
								string ayuda1=arreglostring[0,i];
								string ayuda2=arreglostring[1,i];
								string ayuda3=arreglostring[2,i];
								string ayuda4=arreglostring[3,i];
								string ayuda5=arreglostring[4,i];
								string ayuda6=arreglostring[5,i];
								string ayuda7=arreglostring[6,i];
								string ayuda8=arreglostring[7,i];
								string ayuda9=arreglostring[8,i];

								//sustituye
								arreglostring[0,i]=arreglostring[0,i2];
								arreglostring[1,i]=arreglostring[1,i2];
								arreglostring[2,i]=arreglostring[2,i2];
								arreglostring[3,i]=arreglostring[3,i2];
								arreglostring[4,i]=arreglostring[4,i2];
								arreglostring[5,i]=arreglostring[5,i2];
								arreglostring[6,i]=arreglostring[6,i2];
								arreglostring[7,i]=arreglostring[7,i2];
								arreglostring[8,i]=arreglostring[8,i2];

								//remplaza los que guardaste
								arreglostring[0,i2]=ayuda1;
								arreglostring[1,i2]=ayuda2;
								arreglostring[2,i2]=ayuda3;
								arreglostring[3,i2]=ayuda4;
								arreglostring[4,i2]=ayuda5;
								arreglostring[5,i2]=ayuda6;
								arreglostring[6,i2]=ayuda7;
								arreglostring[7,i2]=ayuda8;
								arreglostring[8,i2]=ayuda9;



							}


						}


				}


			}//termina comparacion y ordenamiento

		}
		Resultado3.Close();
		Resultado3=null;//ANDROID

			//IMPRIMIR->	Text textomuesta=GameObject.FindGameObjectWithTag("textobest"+veces).GetComponent<Text>();
			i3_help=0;
			manda_a_descargar_otra_imagen();
		


	}
	void manda_a_descargar_otra_imagen(){
			

			if(i3_help<5){
				Vector3 v3=new Vector3(BestDish_go.transform.position.x,BestDish_go.transform.position.y+(i3_help*-200f),0);
				GameObject nuevo_bestdish=Instantiate(BestDish_go,v3,BestDish_go.transform.rotation);
				llena_campos_best_dishes script=nuevo_bestdish.GetComponentInChildren<llena_campos_best_dishes>();
				script.estrellas.text=""+arreglostring[5,i3_help]+"("+arreglostring[4,i3_help]+")";//bien
				script.descripcion.text=""+arreglostring[2,i3_help]+",last date rate:"+arreglostring[6,i3_help];
				script.titulo.text=""+arreglostring[1,i3_help];
				script.votos_Cant.text="$"+arreglostring[7,i3_help];//poner precio
				/*
				[2,i3]=nombre ed restaurante
				[3,i3]=Calificacio acumulable
				[1,i3]=nombre del platillo
				[4,i3]=votos
				[6,i3]=fecha
				[7,i3]=precio
				[5,i3]=promedio(Estrellas)
				[0,i3]=id
				[8,i3]=imagen
				
				
				 */
				nuevo_bestdish.transform.SetParent(pantalla_best_dishes.transform);
				nuevo_bestdish.transform.localScale=BestDish_go.transform.localScale;

				descarga_isdone=false;
				
				Debug.Log("textobest"+(i3_help+1));
				//Text textomuesta=GameObject.FindGameObjectWithTag("textobest"+(i3_help+1)).GetComponent<Text>();
				//	string todojunto=""+arreglostring[5,i3_help]+"--"+arreglostring[1,i3_help]+arreglostring[2,i3_help]+arreglostring[3,i3_help]+arreglostring[4,i3_help]+arreglostring[0,i3_help]+arreglostring[6,i3_help]+arreglostring[7,i3_help]+",img:"+arreglostring[8,i3_help];
				//Debug.Log(todojunto);
				//textomuesta.text=todojunto;
				progreso=0f;
					Image imagenqueagarro=nuevo_bestdish.GetComponent<Image>();
					Debug.Log("nameIMG:"+imagenqueagarro.name);
					//Image imagen=GameObject.FindGameObjectWithTag("textobest"+(i3+1)).GetComponent<Image>();
					StartCoroutine(bajaimagen(arreglostring[8,i3_help],imagenqueagarro));

					
					}else{

						if(i3_help==5){
							i3_help=7;

							Debug.Log("ya bajo los 5, puede bajar promociones");
							Debug.Log("i3_help:"+i3_help);
							revisa_pormociones_para_usuario();
							
						}
					}
				
			
	}


	void revisa_pormociones_para_usuario(){

		Debug.Log("entrando a promociones para usuario");



		string _peticion="SELECT restaurante, imagen_promocion, promo, puntos_requeridos,id FROM `promociones` ORDER BY `restaurante` DESC"; 
		adminMySQL _adminMySQL3 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado3=_adminMySQL3.COMANDO(_peticion);
		bool existe=true;
		float posX;
		float posY;
		double r1=1%3;
		Debug.Log("residuo1:"+r1);
		double r2=2%3;
		Debug.Log("residuo2:"+r2);
		double r3=3%3;
		Debug.Log("residuo3:"+r3);
		double r4=4%3;
		Debug.Log("residuo4:"+r4);
		double r5=5%3;
		Debug.Log("residuo5:"+r5);
		double r6=6%3;
		Debug.Log("residuo6:"+r6);
		double r7=7%3;
		Debug.Log("residuo7:"+r7);


		posX=promo_gameobject.transform.position.x;
		posY=promo_gameobject.transform.position.y;
		float xhelper=posX;

		int i_promo=1;
	

		
		if(Resultado3.HasRows)//quitar este if
		{
			string name="";
			int id_anterior=0;
			for(int ix=0;ix<=1000;ix++){
				
			switch(i_promo%3){
				//que equis tendra
				//residuos
				case 1:posX=xhelper;
				if(i_promo!=1){posY-=300f;}
				name="go1";
				
				break;
				case 2:posX=xhelper+250f;name="go2";break;
				case 0:posX=xhelper+500f;name="go3";break;
			}
			
			
			
				
			string xx=Resultado3.ToString();
		
			bool x=Resultado3.Read();
			
			
			//////
			object idobj=Resultado3.GetValue(4);
			int id_numb=int.Parse(idobj.ToString());
			Debug.Log("ID:"+id_numb);
		
			if(id_numb!=id_anterior){
				//si no se repite el id, significa que no ha terminado de leer la BD
				id_anterior=id_numb;
			object obj_rest=Resultado3.GetValue(0);
		
			string restaurante=obj_rest.ToString();
			Debug.Log("restaurante:"+restaurante);
				
			object obj_imagen=Resultado3.GetValue(1);
			string imagen_string=obj_imagen.ToString();
			object obj_promo=Resultado3.GetValue(2);
			string promocion_s=obj_promo.ToString();
			object obj_pts=Resultado3.GetValue(3);
			string puntos_s=obj_pts.ToString();
			double puntos_double=double.Parse(puntos_s);


			Vector3 go_pos=new Vector3(posX,posY,0);
			GameObject ng=Instantiate(promo_gameobject,go_pos,promo_gameobject.transform.rotation);
			ng.transform.SetParent(pantalla_promociones.transform);
			ng.transform.localScale=promo_gameobject.transform.localScale;
			ng.name=name;
			Text textopt=ng.GetComponentInChildren<Text>();
			textopt.text=""+puntos_s;
			i_promo++;
			Image imagen_obj=ng.GetComponent<Image>();
			promo promoscript=ng.GetComponentInChildren<promo>();
			promoscript.restaurant_De_promo=restaurante;
			promoscript.promocion_querida=promocion_s;
			promoscript.puntos_requeridos=""+puntos_double;

			StartCoroutine(bajaimagen(imagen_string,imagen_obj));

			}//if
			else{ix=1002;Debug.Log("idanterior:"+id_anterior);}
				
			}//for
			}
		globalvariables.segunda_revision=false;
		Debug.Log("segunda revision false");
		Debug.Log("*******************************************************************");

		Resultado3.Close();
		Resultado3=null;//ANDROID










	}
	IEnumerator bajaimagen(string url, Image imagen){
		Debug.Log("entra a metodo bajaimagen");
		Debug.Log("url:"+url);
		 using (WWW www = new WWW(url))
        {
			Debug.Log("yield");
            // Wait for download to complete
            yield return www;

            // assign texture
			if(www.isDone){
           imagen.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
			descarga_isdone=true;
			i3_help++;
			Debug.Log("descarga terminada!");
			}else{
				descarga_isdone=false;
				//Debug.Log("%:"+www.progress);
				progreso=www.progress;
			}
			
            
			
        }
		

	}

	public void compara_version(){
		globalvariables.ultima_version_revisada_bool=false;
		ventana_menu=false;
		Debug.Log("checare la version mas reciente de la BD");
		string _peticion="SELECT version, liga_android, liga_ios FROM versiones WHERE actual=true";
		adminMySQL _adminMySQL3 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado3=_adminMySQL3.COMANDO(_peticion);
		bool existe=true;
		if(Resultado3.HasRows){
			//pasaralasiguiente escena
				/*NECESARY SHIT */
				string xx=Resultado3.ToString();
		
			bool x=Resultado3.Read();
			
			//////
			object valor1=Resultado3.GetValue(0);
			string ultima_version=valor1.ToString();

			//numero de version
			globalvariables.version_bd=double.Parse(ultima_version);
			Debug.Log("la ultima version es "+globalvariables.version_bd);
		

			object valor2=Resultado3.GetValue(1);
			string ligaa_android_s=valor2.ToString();

			object valor3=Resultado3.GetValue(2);
			string ligaa_ios_s=valor3.ToString();

			if(es_android){
				globalvariables.liga_assetbundle=ligaa_android_s;

			}else{
				globalvariables.liga_assetbundle=ligaa_ios_s;
			}
			
			}else{Debug.Log("no encontre en bd nada");}
			Resultado3.Close();
			Resultado3=null;
			globalvariables.ultima_version_revisada_bool=true;

	}


	public void descarga_menuAR(){
		ventana_AR=false;
		//prueba borrar despues
		//globalvariables.global_restaurante_Actual="tpt";
		//busca el nombre del restaurant en la BD, toma su liga de github y descarga el assetbundle
		Debug.Log("RESTAURANTE_ACTUAL:"+globalvariables.global_restaurante_Actual);
		//prueba
			string _peticion="SELECT menu_pesado_iOS,menu_pesado_android FROM assetBundles WHERE Restaurant='"+globalvariables.global_restaurante_Actual+"';";
			Debug.Log("se descargara el menu de "+globalvariables.global_restaurante_Actual);
		adminMySQL _adminMySQL3 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado3=_adminMySQL3.COMANDO(_peticion);
		bool existe=true;
		if(Resultado3.HasRows){
			//pasaralasiguiente escena
				/*NECESARY SHIT */
				string xx=Resultado3.ToString();
		
			bool x=Resultado3.Read();
			
			//////
			object valor1=Resultado3.GetValue(0);
			string iOS_string=valor1.ToString();
			object valor2=Resultado3.GetValue(1);
			string Android_string=valor2.ToString();
			


		if(es_android){
			//descarga android AR
			globalvariables.liga_assetbundle=Android_string;
		}else{
			//descarga ios AR
			globalvariables.liga_assetbundle=iOS_string;

		}
		globalvariables.hora_de_descargar_assetbundle=true;
		Debug.Log("LIGA DE BD:"+globalvariables.liga_assetbundle);
		Resultado3.Close();
		Resultado3=null;
		}else{Resultado3.Close();
		Resultado3=null;
		Debug.Log("no existe ese restaurante");
		}

	}

	public void updatetable(){
			//string _insert="UPDATE usuarios SET usuario = 'juanki', puntuacion = 125 WHERE id=7;";
						string _insert="UPDATE usuarios SET puntuacion = '"+inputpuntuacion.text+"' WHERE usuario='"+inputusuario.text+"';";

		adminMySQL _adminMySQL =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado=_adminMySQL.COMANDO(_insert);
		Debug.Log("actualizado!");
	
		Resultado.Close();
		Resultado=null;//android
	}


	public void revisa_si_es_admin(){
		string correo=globalvariables.usuario_actual_restaurant;
		string _peticion="SELECT modalidad FROM usuarios_restaurantes WHERE correo='"+correo+"';";
		adminMySQL _adminMySQL3 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado3=_adminMySQL3.COMANDO(_peticion);
		bool existe=true;
		

		if(Resultado3.HasRows){
			//pasaralasiguiente escena
				/*NECESARY SHIT */
				string xx=Resultado3.ToString();
			Debug.Log("XXX");
			Debug.Log(xx+"xd");
			bool x=Resultado3.Read();
			Debug.Log(x);
			//////
			object valor1=Resultado3.GetValue(0);
			string modalidad=valor1.ToString();

			if(modalidad=="admin"){
			Debug.Log("es admin");
			globalvariables.es_admin_bool=true;}
			else{globalvariables.es_admin_bool=false;
			Debug.Log("no es admin");}

		}else{Debug.Log("no es admin");
		globalvariables.es_admin_bool=false;}
		Resultado3.Close();
		Resultado3=null;//ANDROID

	}
	public void login_as_restaurant(){
		string correo=userRest.text;
		string contraseña=passRest.text;
		string _peticion="SELECT password, restaurant FROM usuarios_restaurantes WHERE correo='"+correo+"';" ;

		adminMySQL _adminMySQL3 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado3=_adminMySQL3.COMANDO(_peticion);
		bool existe=true;
		if(Resultado3.HasRows){
			//pasaralasiguiente escena
			//COMPARA EL PASSWORD
			
			Debug.Log("correo correcto");

			
			/*NECESARY SHIT */
				string xx=Resultado3.ToString();
			Debug.Log("XXX");
			Debug.Log(xx+"xd");
			bool x=Resultado3.Read();
			Debug.Log(x);
			//////
			object passwordobj=Resultado3.GetValue(0);
			string passwordRecibida=passwordobj.ToString();

			//COMPARA PASSWORDS
			if(contraseña==passwordRecibida){



			object valor1=Resultado3.GetValue(1);
			globalvariables.global_restaurante_Actual=valor1.ToString();
			
			


			//el usuario sera el correo
			globalvariables.usuario_actual_restaurant=correo;
			SceneManager.LoadScene("mesero");
		}else{Debug.Log("PASSWORD INCORRECTA!");}
			
		//HASROWS
		}else{Debug.Log("correo o password incorrecto!");}
		Resultado3.Close();
		Resultado3=null;//android
		

	}
	public void LogOut(){
		globalvariables.global_restaurante_Actual="";
		globalvariables.nombre_deUsuario="";
		globalvariables.usuario_actual_restaurant="";
		SceneManager.LoadScene("facebook_prueba");
	}
	public void agregar_mesero_a_lista(){
		string mesero_name=inputusuario.text;
		string contraseña=inputpuntuacion.text;
		string confcontraseña=inputplatillo.text;

		//prueba, borrar despues
		 globalvariables.usuario_actual_restaurant="admin.tpt@blackdish.com";
		 globalvariables.global_restaurante_Actual="tpt";
		//anterior prueba borrar

		if(contraseña==confcontraseña){
			string usuario=mesero_name+"."+globalvariables.global_restaurante_Actual;
			string mail=usuario+"@blackdish.com";
			string restaurant=globalvariables.global_restaurante_Actual;
			string modalidad="mesero";
			string Date=System.DateTime.Now.ToString();

			
			string _insert="INSERT INTO usuarios_restaurantes (usuario,restaurant,correo,password,modalidad,fecha_de_registro,registrado_por) VALUES ('"+usuario+"','"+restaurant+"','"+mail+"','"+contraseña+"','"+modalidad+"','"+Date+"','"+globalvariables.usuario_actual_restaurant+"');";
		adminMySQL _adminMySQL =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado=_adminMySQL.COMANDO(_insert);
		Debug.Log("insertado!");
	
		Resultado.Close();
		Resultado=null;//android



		}




	}

	public void insertar(){//string _log="SELECT * FROM `usuarios` WHERE `usuario` LIKE '"+usuarioTXT.text+"' AND `puntuacion` LIKE "+contraseñaTXT.text;
		string _insert="INSERT INTO usuarios (usuario,puntuacion) VALUES ('"+inputusuario.text+"',"+inputpuntuacion.text+");";
		adminMySQL _adminMySQL =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado=_adminMySQL.COMANDO(_insert);
		Debug.Log("insertado!");
	
		Resultado.Close();
		Resultado=null;//android
		
		//este error es para indicarte que aqui te quedaste
}

public void insertar_actual_user(){
		string date=System.DateTime.Now.ToString();
		//string _log="SELECT * FROM `usuarios` WHERE `usuario` LIKE '"+usuarioTXT.text+"' AND `puntuacion` LIKE "+contraseñaTXT.text;
		string _insert="INSERT INTO usuarios (id_fb,usuario,puntuacion,Registro_date) VALUES ("+globalvariables.id_fb+",'"+globalvariables.nombre_deUsuario+"', 0,'"+date+"' );";
		adminMySQL _adminMySQL =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado=_adminMySQL.COMANDO(_insert);
		Debug.Log("insertado "+globalvariables.nombre_deUsuario+"!");
	
		Resultado.Close();
		Resultado=null;//android
		
		//este error es para indicarte que aqui te quedaste
}





	public void agrega_usuario_si_no_esta(){
		
		string Date=System.DateTime.Now.ToString();
		string _peticion="SELECT usuario FROM usuarios WHERE usuario='"+globalvariables.nombre_deUsuario+"';" ;

		adminMySQL _adminMySQL3 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado3=_adminMySQL3.COMANDO(_peticion);
		bool existe=true;
		if(Resultado3.HasRows){
			string xx=Resultado3.ToString();
			Debug.Log(xx+" si existe");
			existe=true;
			}else{
				Debug.Log("NO EXISTE");
				Debug.Log("se agregara");
				existe=false;
			}
	
		Resultado3.Close();
		Resultado3=null;
		if(!existe){
			insertar_actual_user();
		}

	}
	

public void actualiza_promedio_del_platillo(){
	string promstring="";
	string _peticion="SELECT promedio FROM scores WHERE platillo='"+movefood.platillo_actual+"' AND restaurante='"+movefood.restaurant_actual+"';";
	adminMySQL _adminMySQL10 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado10=_adminMySQL10.COMANDO(_peticion);
		Debug.Log("comando 10");
		if(Resultado10.HasRows){
			Debug.Log("comando10 has rows");
			
			/*NECESARY SHIT */
				string xx=Resultado10.ToString();
			Debug.Log("XXX");
			Debug.Log(xx+"xd");
			bool x=Resultado10.Read();
			Debug.Log(x);
			//////
			object valor1=Resultado10.GetValue(0);
			promstring=valor1.ToString();
		}else{promstring="0";}
		Resultado10.Close();
		Resultado10=null;//android
		double prom=double.Parse(promstring);
		movefood.promedio_Actual=prom;
		



}
public void votacion_online(int califa){

	if(login_db.sepuedecalificar){
		bool could_read=false;
		int calificacion_otorgada=califa;
		double promedio_actual=0;
		Debug.Log("votacion");
	
		string Date=System.DateTime.Now.ToString();
		double calificacion_acumulables=0;
		int total_votos=0;
		
		string _peticion="SELECT calificacion_acumulable, total_votos FROM scores WHERE platillo='"+movefood.platillo_actual+"' AND restaurante='"+movefood.restaurant_actual+"';" ;
		adminMySQL _adminMySQL10 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado10=_adminMySQL10.COMANDO(_peticion);
		Debug.Log("comando 10");
		if(Resultado10.HasRows){
			Debug.Log("comando10 has rows");
			
			/*NECESARY SHIT */
				string xx=Resultado10.ToString();
			Debug.Log("XXX");
			Debug.Log(xx+"xd");
			bool x=Resultado10.Read();
			Debug.Log(x);
			//////
			object valor1=Resultado10.GetValue(0);
			string calstring=valor1.ToString();
			calificacion_acumulables=double.Parse(calstring);
			object valor2 =Resultado10.GetValue(1);
			string total_votosstring=valor2.ToString();
			total_votos=int.Parse(total_votosstring);
			could_read=true;
		}
		Resultado10.Close();
		Resultado10=null;//android
		if(could_read){
			//si se pudo leer, ahora subo la nueva calificacion
		calificacion_acumulables+=calificacion_otorgada;
		total_votos+=1;
		promedio_actual=calificacion_acumulables/total_votos;
		string promstring=""+promedio_actual;
		string promstringn_send="";

		for(int i=0; i<=promstring.Length-1;i++){
			if(promstring[i]!='.'){
				promstringn_send+=promstring[i];
			}else{
				string two_decimals="."+promstring[i+1]+promstring[i+1];
				promstringn_send+=two_decimals;
				i=promstring.Length+1;
			}
		}
		Debug.Log("-------");
		Debug.Log("promedio"+promstringn_send);
		double doublenprom=double.Parse(promstringn_send);
		Debug.Log("doubleprom="+doublenprom);

		string _sube_calificacion="UPDATE scores SET calificacion_acumulable="+calificacion_acumulables+",total_votos="+total_votos+",promedio="+doublenprom+",fecha_last_vote='"+Date+"' WHERE platillo='"+movefood.platillo_actual+"' AND restaurante='"+movefood.restaurant_actual+"';" ;
		adminMySQL _adminMySQL11 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado11=_adminMySQL11.COMANDO(_sube_calificacion);
		Resultado11.Close();
		Resultado11=null;//android
		Debug.Log("se califico el platillo");


		//REGISTRA EN LA LISTA EL VOTO
		string _string_registro="INSERT INTO lista_de_votos (usuario,fb_id,platillo,restaurante,voto_emitido,fecha) VALUES('"+globalvariables.nombre_deUsuario+"','"+globalvariables.id_fb+"','"+movefood.platillo_actual+"','"+movefood.restaurant_actual+"',"+calificacion_otorgada+",'"+Date+"');";
		adminMySQL _adminMySQL12=administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado12=_adminMySQL12.COMANDO(_string_registro);
		Resultado12.Close();
		Resultado12=null;//android



		}else{
			Debug.Log("no aparecio ese platillo");
			Debug.Log("Se agrega nuevo");
			string agrega="INSERT INTO scores (platillo,restaurante,calificacion_acumulable,total_votos,promedio,fecha_last_vote) VALUES('"+movefood.platillo_actual+"','"+movefood.restaurant_actual+"',"+calificacion_otorgada+","+1+","+calificacion_otorgada+",'"+Date+"');";
			//si es pruebas
			adminMySQL _adminMySQL4=administradorbasededatos.GetComponent<adminMySQL>();
			MySqlDataReader Resultadohelp11=_adminMySQL4.COMANDO(agrega);
			Resultadohelp11.Close();
			Resultadohelp11=null;//android

		}
	


	}//sepuedecalificar
	else{

		notificacion_game_object.SetActive(true);
		Text texto=GameObject.FindGameObjectWithTag("textonotficacion").GetComponent<Text>();
		texto.text="For Rate this Dish, You have to buy it first";
	}

}
public void usuario_recibe_puntos(){
	//ya estaba echo cuando el meesero califica
	int intnotificaciones=0;

	string _log_checa_not="SELECT notificaciones FROM usuarios WHERE id_fb='"+globalvariables.id_fb+"';";
	adminMySQL _adminMySQL8 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado8=_adminMySQL8.COMANDO(_log_checa_not);
		if(Resultado8.HasRows){

				string xx=Resultado8.ToString();
			Debug.Log("XXX");
			Debug.Log(xx+"xd");
			bool x=Resultado8.Read();
			Debug.Log(x);
			object valor =Resultado8.GetValue(0);
			//object ddds=Resultado.GetValue(1);
			string valor_string=valor.ToString(); 
			
			object valor_not=Resultado8.GetValue(0);
			string stringnotx=valor_not.ToString();
			 intnotificaciones=int.Parse(stringnotx);
		}
		Resultado8.Close();
		Resultado8=null;//android
		
		intnotificaciones=busca_la_notificacion_en_restaurantes(intnotificaciones);
		

	string log_actualiza_not="UPDATE usuarios SET notificaciones="+intnotificaciones+" WHERE id_fb='"+globalvariables.id_fb+"';";
	adminMySQL _adminMySQL9=administradorbasededatos.GetComponent<adminMySQL>();
	MySqlDataReader Resultado9=_adminMySQL9.COMANDO(log_actualiza_not);
	Resultado9.Close();
	Resultado9=null;//android


}
public int busca_la_notificacion_en_restaurantes(int intnotificaciones){
	Debug.Log("*****************");
	Debug.Log("entra al metodo notificacion");


	//E RROR INTENCIONAL PARA QUE SEPAS DONDE TE QUEDASTE->
	/*
	REVISAR EN TABLA USUARIOS SI TIENE NOTIFICACIONES
	SI SI LAS TIENE EJECUTA EL CODIGO QUE ESTA ESCRITO JUSTO ABAJO
	AL TERMINAR, RESTA -1 A LAS NOTIFICACIONES DE LA TABLA USUARIOS
	 */


	//BUSCA EN TPT
	bool recibido_de_tpt=false;
	string date_ask="";
	string _log="SELECT DATE_ASK, puntos_pedidos, restaurante FROM pedidos_tpt WHERE id_fb='"+globalvariables.id_fb+"' AND puntos_aprobados>0 AND visto_por_usuario=false;";
	adminMySQL _adminMySQL7 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado7=_adminMySQL7.COMANDO(_log);
		Debug.Log("comando 7");
		if(Resultado7.HasRows){
			Debug.Log("comando7 has rows");
			recibido_de_tpt=true;
			/*NECESARY SHIT */
				string xx=Resultado7.ToString();
			Debug.Log("XXX");
			Debug.Log(xx+"xd");
			bool x=Resultado7.Read();
			Debug.Log(x);
			//////
			object valorfecha=Resultado7.GetValue(0);
			date_ask=valorfecha.ToString();
			object valor =Resultado7.GetValue(1);
			//object ddds=Resultado.GetValue(1);
			string valor_string=valor.ToString();
			int puntos_de_ese_valor=int.Parse(valor_string);

			object valorrest=Resultado7.GetValue(2);
			string rest_name=valorrest.ToString();
			
			Debug.Log("NOTIFICACION!");
			Debug.Log("*************");
			Debug.Log("haz recibido "+ valor_string+" puntos de "+rest_name+"!");
			Debug.Log("*************");
			Debug.Log("NOTIFICACION!");

			notificacion_game_object.SetActive(true);
			
			Text textonot=GameObject.FindGameObjectWithTag("textonotficacion").GetComponent<Text>();
			textonot.text="haz recibido "+ valor_string+" puntos de TPT!";
			//MOSTRAR LA VENTANA DE HAZ RECIBIDO
			}
			Resultado7.Close();
			Resultado7=null;//android
			Debug.Log("comando 7 close");
			if(recibido_de_tpt){
				Debug.Log("si se recibio");

				string _log2="UPDATE pedidos_tpt SET visto_por_usuario=true WHERE id_fb='"+globalvariables.id_fb+"' AND DATE_ASK='"+date_ask+"';";
				adminMySQL _updatedate=administradorbasededatos.GetComponent<adminMySQL>();
				MySqlDataReader resultado8=_updatedate.COMANDO(_log2);
				resultado8.Close();
				resultado8=null;//android
				Debug.Log("comando8 close");
				intnotificaciones--;
				
			}
			
			return intnotificaciones;







}
public void usuario_pide(string motivo_orden_o_promocion_public){
		string puntos_de_promo=globalvariables.puntos_pedidos_a_promo;
		string Date=System.DateTime.Now.ToString();
		string username="";
		if(screenusuario){
			username=globalvariables.nombre_deUsuario;
			if(username==""||username==null){
				int random=Random.RandomRange(10000,20000);
				username="usuario_prueba"+random;
			}

		}else{
			//username=inputusuario.text;
			username="usuariotest";
			
		}
		if(globalvariables.id_fb==null){
			int idx=Random.Range(1250843,9999999);
			globalvariables.id_fb="p"+idx;
			
			Debug.Log("error A");
			string _nuevo_usuario="INSERT INTO usuarios (id_fb,usuario,puntuacion, Registro_date, last_use) VALUES('"+globalvariables.id_fb+"','"+username+"',0,'"+Date+"','"+Date+"');";
			//si es pruebas
			adminMySQL _adminMySQL4=administradorbasededatos.GetComponent<adminMySQL>();
			MySqlDataReader Resultadohelp=_adminMySQL4.COMANDO(_nuevo_usuario);
			Resultadohelp.Close();
			Resultadohelp=null;//android
			
			
			}else{

				Debug.Log("error B");
			string _uso_usuario="UPDATE usuarios SET last_use='"+Date+"' WHERE id_fb='"+globalvariables.id_fb+"';";
			adminMySQL _adminMySQL4=administradorbasededatos.GetComponent<adminMySQL>();
			MySqlDataReader Resultadohelp=_adminMySQL4.COMANDO(_uso_usuario);
			Resultadohelp.Close();
			Resultadohelp=null;//android
			}

			Debug.Log("ERROR C");



		string platillo_a_ordenar;
		string puntos_a_pedir;
		if(screenusuario){
			platillo_a_ordenar=movefood.platillo_actual;
			if(motivo_orden_o_promocion_public=="promocion"){
				puntos_a_pedir=puntos_de_promo;
			}
			else{
			puntos_a_pedir=""+movefood.puntosactuales;}
		}else{
			platillo_a_ordenar=inputplatillo.text;
			puntos_a_pedir=inputpuntuacion.text;
		}
		string _peticion="INSERT INTO pedidos_tpt (DATE_ASK,usuario,platillo,puntos_pedidos,id_fb,motivo_orden_o_promocion,restaurante) VALUES('"+Date+"','"+username+"','"+platillo_a_ordenar+"',"+puntos_a_pedir+",'"+globalvariables.id_fb+"','"+motivo_orden_o_promocion_public+"','"+globalvariables.global_restaurante_Actual+"');";
		Debug.Log("peticion:"+_peticion);
		adminMySQL _adminMySQL =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado=_adminMySQL.COMANDO(_peticion);
		Debug.Log("insertado!");
	
		Resultado.Close();
		Resultado=null;//android

		
		

	}
public void mesero_aprueba(){
		bool recibido_por_mesero_bool=true;
		bool respuesta_mesero_bool=true;
		mesero_envia(recibido_por_mesero_bool,respuesta_mesero_bool);
		inputplatillo.text="";
		inputpuntuacion.text="";
		inputusuario.text="";
	}

public void mesero_rechaza(){
		bool recibido_por_mesero_bool=true;
		bool respuesta_mesero_bool=false;
		mesero_envia(recibido_por_mesero_bool,respuesta_mesero_bool);
			inputplatillo.text="";
		inputpuntuacion.text="";
		inputusuario.text="";
	}
public void mesero_envia(bool recibido_por_mesero_bool,bool respuesta_mesero_bool){
		string date_actual=System.DateTime.Now.ToString();
		int puntos_dados=0;
		if(respuesta_mesero_bool){
			puntos_dados=int.Parse(inputpuntuacion.text);
		}
		
		
		
		string _insert="UPDATE pedidos_tpt SET recibido_por_mesero ="+recibido_por_mesero_bool+",puntos_aprobados="+puntos_dados+", respuesta_mesero="+respuesta_mesero_bool+", DATE_ANSWER='"+date_actual+"',nombre_mesero='"+globalvariables.usuario_actual_restaurant+"' WHERE id_fb="+id_fb_mesero_actual+";";
		adminMySQL _adminMySQL =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado=_adminMySQL.COMANDO(_insert);
		Debug.Log("actualizado!");
	
		Resultado.Close();
		Resultado=null;//android

		if(respuesta_mesero_bool){

			string _log="SELECT puntuacion FROM usuarios WHERE usuario='"+inputusuario.text+"';";

		adminMySQL _adminMySQL3 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado3=_adminMySQL.COMANDO(_log);
		if(Resultado3.HasRows){
			string xx=Resultado3.ToString();
			Debug.Log("XXX");
			Debug.Log(xx+"xd");
			bool x=Resultado3.Read();
			Debug.Log(x);
			object valor =Resultado3.GetValue(0);
			//object ddds=Resultado.GetValue(1);
			string valor_string=valor.ToString();
			int puntos_de_ese_valor=int.Parse(valor_string);
			puntos_dados+=puntos_de_ese_valor;
			Resultado3.Close();
			Resultado3=null;//android
			}else{Resultado3.Close();
			Resultado3=null;//android
			}

		//////////







			string _insert2="UPDATE usuarios SET puntuacion = "+puntos_dados+" WHERE id_fb='"+id_fb_mesero_actual+"';";

		adminMySQL _adminMySQL2 =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado2=_adminMySQL.COMANDO(_insert2);
		Debug.Log("actualizado!");
	
		Resultado2.Close();
		Resultado2=null;//android





		//NOTIFICACION
		Debug.Log("----------------------------");
		Debug.Log("NOTIFICACION");
		int notificacionesint=0;
		Debug.Log("selecciona not");
		string stringpunts="SELECT notificaciones FROM usuarios WHERE id_fb='"+id_fb_mesero_actual+"';";
		adminMySQL _admin55=administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado55=_admin55.COMANDO(stringpunts);
		if(Resultado55.HasRows){
			string xx3=Resultado55.ToString();
			Debug.Log("XXX");
			Debug.Log(xx3+"xd");
			bool xxx3=Resultado55.Read();
			Debug.Log(xxx3);
			Debug.Log("si hay algo que incrementar");
			object valor2 =Resultado55.GetValue(0);//USUARIO
			Debug.Log("object");
			string valors=valor2.ToString();
			Debug.Log(valors);
			notificacionesint=int.Parse(valors);
			Debug.Log("se convirtio a entero");
			Resultado55.Close();
			Resultado55=null;//android
		}
		
		notificacionesint++;
		Debug.Log("se sumo!");
		string updateNot="UPDATE usuarios SET notificaciones ='"+notificacionesint+"' WHERE id_fb='"+id_fb_mesero_actual+"';";
		adminMySQL _adminNOT=administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado_notif=_adminNOT.COMANDO(updateNot);
		Resultado_notif.Close();
		Resultado_notif=null;//android
		Debug.Log("se actualizo notificacion!");








		
		}

	}


	
public void revisa_pedidos(){
	globalvariables.global_restaurante_Actual="tpt";


	/* MySqlConnection conect = new MySqlConnection("server=sql9.freemysqlhosting.net;uid=sql9270836;pwd=Z7iVjKr9fL;database=sql9270836");
	MySqlCommand insere=new MySqlCommand();
	insere.Connection=conect;
	insere.CommandText="SELECT usuario, puntos_pedidos, platillo, id_fb  FROM pedidos_tpt WHERE recibido_por_mesero=0 AND restaurante='"+globalvariables.global_restaurante_Actual+"';";
	insere.ExecuteNonQuery();
	conect.Close();
	*/

		string _log="SELECT usuario, puntos_pedidos, platillo, id_fb  FROM pedidos_tpt WHERE recibido_por_mesero=0 AND restaurante='"+globalvariables.global_restaurante_Actual+"';";
		Debug.Log("rest_Actual:"+globalvariables.global_restaurante_Actual);
			


			adminMySQL _adminNOT=administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado=_adminNOT.COMANDO(_log);
			//nose que pasa
			try{
				text_error.text+="mando a llamar a mysql";
				//android
				
		
		//termina cambio de android
		
		text_error.text+="/read:"+Resultado.Read();
		text_error.text+="/lifetimese:"+Resultado.GetLifetimeService();
		text_error.text+="/tostring:"+Resultado.ToString();
		text_error.text+="/getva:"+Resultado.GetValue(0);
		text_error.text+="/VFC:"+Resultado.VisibleFieldCount.ToString();
		
			if(Resultado.HasRows){
			string xx=Resultado.ToString();
			//Debug.Log("XXX");
		//	Debug.Log(xx+"xd");
			bool x=Resultado.Read();
			//Debug.Log(x);
			object valor =Resultado.GetValue(0);//USUARIO
			inputusuario.text=valor.ToString();
			
			object valor2= Resultado.GetValue(1);
			inputpuntuacion.text=valor2.ToString();
			object valor3=Resultado.GetValue(2);
			inputplatillo.text=valor3.ToString();

			object valor4=Resultado.GetValue(3);
			id_fb_mesero_actual=valor4.ToString();

			//object ddds=Resultado.GetValue(1);
			string valor_string=valor.ToString()+","+valor2.ToString();

			//string xxd2=ddds.ToString();
		//	Debug.Log(valor_string);
			textopuntuacion.text=valor_string;
			//Debug.Log("Login correcto");
		//	Debug.Log("R:"+Resultado);
			}
			Resultado.Close();
			Resultado=null;//android
			text_error.text+="no hubo error";}
			catch(MySqlException e){
				text_error.text+=""+e;
			}
	}
public void seleccionar(){
		//string _log="SELECT * FROM `usuarios` WHERE `usuario` LIKE '"+usuarioTXT.text+"' AND `puntuacion` LIKE "+contraseñaTXT.text;
		//string _log="SELECT puntuacion FROM usuarios WHERE usuario='"+inputusuario.text+"' AND id=1;";
				string _log="SELECT puntuacion FROM usuarios WHERE usuario='"+inputusuario.text+"';";

		adminMySQL _adminMySQL =administradorbasededatos.GetComponent<adminMySQL>();
		MySqlDataReader Resultado=_adminMySQL.COMANDO(_log);
	

		if(Resultado.HasRows){
			string xx=Resultado.ToString();
			Debug.Log("XXX");
			Debug.Log(xx+"xd");
			bool x=Resultado.Read();
			Debug.Log(x);
			object valor =Resultado.GetValue(0);
			//object ddds=Resultado.GetValue(1);
			string valor_string=valor.ToString();
			//string xxd2=ddds.ToString();
			Debug.Log(valor_string);
			textopuntuacion.text=valor_string;
			Debug.Log("Login correcto");
			Debug.Log("R:"+Resultado);
			Resultado.Close();
			Resultado=null;//android


		}else{
			Debug.Log("Usuario o contraseña incorrectos");
			Resultado.Close();
			Resultado=null;//android
		}
		//este error es para indicarte que aqui te quedaste

	
	}
public void select_metodo(){
		string URL="http://localhost/mydb/userSelect.php";
		select_metodo_ienumarator(URL);

	}

	IEnumerator select_metodo_ienumarator(string url){
		WWW users =new WWW(url);
		yield return users;
		string userDatastring=users.text;
		userData = userDatastring.Split(';');

	}
void Start () {
	globalvariables.descargare_el=descargare_el;
		tiempo=60;
		tiempo2=0;
		login_db.bajapromedio=false;
		
		
	}
	
	// Update is called once per frame
void Update () {
		//tiempotext.text=""+tiempo;
		if(screenusuario){
			
			if(!globalvariables.revisando_version_en_proceso&&!globalvariables.segunda_revision){
			tiempo+=Time.deltaTime;
			if(tiempo>70){
				tiempo=0;
				usuario_recibe_puntos();
				
			}
			
			


			}
		/*	if(tiempo2>=3){
				Debug.Log("***mas valorados:");
				c
				
			}else{
				if(tiempo2<2){Debug.Log("***tiempo2<2");}
				if(tiempo<3){
				tiempo2+=Time.deltaTime;}
			}*/


		}
		if(login_db.bajapromedio){
			actualiza_promedio_del_platillo();
			login_db.bajapromedio=false;
		}

		if(screenrestaurante){
			globalvariables.activacampos=false;
			tiempo+=Time.deltaTime;
			if(tiempo>3){
			revisa_si_es_admin();
			Debug.Log("revisando si es admin");
			screenrestaurante=false;
			Debug.Log("screenrest:"+screenrestaurante);
			globalvariables.activacampos=true;}
		}
		if(ventana_AR){
			globalvariables.global_ventana_menu=false;
			
			tiempo+=Time.deltaTime;
			if(tiempo>3){
			descarga_menuAR();
			ventana_AR=false;}
		}
		if(ventana_menu){
			globalvariables.global_ventana_menu=true;
					tiempo+=Time.deltaTime;
			if(tiempo>3){
				Debug.Log("***checare la ultima version***");
			compara_version();
			ventana_menu=false;}

		}
		if(descarga_isdone){
		manda_a_descargar_otra_imagen();
		}

		if(globalvariables.descarga_mejores_platillos){
			muestra_los_mas_valorados();
			globalvariables.descarga_mejores_platillos=false;
		}
			
			
			
				
	}
}
