# 🎳 Super Rodolfo Strike

![Unity](https://img.shields.io/badge/Unity-6000.0.60f1-black.svg?style=for-the-badge\&logo=unity)
![Platform](https://img.shields.io/badge/Platform-Android-green.svg?style=for-the-badge\&logo=android)

**Super Rodolfo Strike** es un videojuego táctico de físicas en 2D para **dispositivos Android**. El objetivo es lanzar a nuestro protagonista como si fuera una bola de bolos para **derribar, empujar y colocar bolos** en zonas estratégicas y optimizando la puntuación en tiempo real.

Un juego pensado para partidas rápidas, precisión táctil y físicas con mucho carácter. 💥

> 🌟 **Nota:** Este título es un **Spin-off oficial** de la saga principal.

## 🎓 Origen del Proyecto

Este videojuego ha sido desarrollado como proyecto práctico dentro del **Curso de Especialización en Desarrollo de Videojuegos y Realidad Virtual** en el **IES Virgen del Carmen**.

## 🎮 ¡Juega ahora! (APK)

Si solo quieres jugar y no abrir el proyecto en Unity:

1. Entra en la carpeta **`/APK`** del repositorio.
2. Descarga el archivo con extensión **`.apk`**.
3. Instálalo en tu dispositivo Android.
4. Asegúrate de permitir **instalación desde orígenes desconocidos**.
5. ¡Listo! 🎉 A jugar.

---

## 📥 Descargar el código fuente

Si quieres trastear con el proyecto o aprender cómo está hecho:

### 🔹 Opción A: Descarga directa (ZIP)

1. Pulsa el botón verde **Code**.
2. Selecciona **Download ZIP**.
3. Descomprime el archivo.
4. Abre la carpeta desde **Unity Hub**.

### 🔹 Opción B: Clonar repositorio (Terminal)

```bash
git clone https://github.com/djsekai34/SuperRodolfoStrike.git
```

Luego:

1. Abre **Unity Hub**.
2. Pulsa **Add / Añadir proyecto**.
3. Selecciona la carpeta clonada.
4. ⚠️ **Importante:** Abre el proyecto con **Unity 6 (6000.0.60f1)** o superior. Si no tienes esa versión exacta, el Hub te pedirá descargarla o realizar una actualización del proyecto (se recomienda usar la versión original para evitar errores de compatibilidad en las físicas).

> Usar otra versión puede provocar comportamientos inesperados en las físicas.

---

## 🚀 Características principales

🎯 **Sistema de Lanzamiento Táctico**
Mecánica tipo *tirachinas* optimizada para pantallas táctiles usando el **nuevo Input System** de Unity.

🧠 **Físicas Inteligentes & Anti‑Cheat**

* Durante el arrastre, la bola ignora colisiones para evitar exploits.
* El disparo solo se ejecuta en condiciones válidas.

👻 **Modo Fantasma de Plataformas**

* Las plataformas se vuelven intangibles al entrar en la zona de meta.
* Permiten el paso de objetos sin perder estabilidad en el suelo.

🎵 **Gestión de Audio Persistente**

* Música continua entre escenas.
* Implementado con **Singleton + DontDestroyOnLoad**.

📊 **Sistema de Puntuación Preciso**

* Detección exacta de bolos en la zona de victoria.
* Cálculo mediante **Bounds** para máxima fiabilidad.

---

## 🛠️ Tecnologías utilizadas

* **Motor:** Unity 6 (6000.0.60f1)
* **Lenguaje:** C#
* **Input:** Unity Input System (Touchscreen)
* **Físicas:** Physics 2D

  * `Rigidbody2D`
  * `SpringJoint2D`

---

## 📂 Scripts principales

| Script                  | Descripción                                                                  |
| ----------------------- | ---------------------------------------------------------------------------- |
| `ControlLanzamiento.cs` | Controla el agarre, arrastre y disparo de la bola con protección de físicas. |
| `Bolo.cs`               | Gestiona la puntuación y detección de bolos en la zona objetivo.             |
| `Plataforma.cs`         | Controla profundidad visual y colisiones inteligentes.                       |
| `MusicManager.cs`       | Gestor de música persistente entre escenas.                                  |
| `GameManager.cs`        | Control central del juego: tiempo, puntos y estados.                         |

---

## 📱 Plataforma objetivo

✔️ Android
✔️ Controles 100% táctiles
✔️ Optimizado para móviles

---

## 👤 Contacto y Redes

¡Hola! Soy **David**, el desarrollador detrás de este Spin-off. Si quieres charlar sobre desarrollo de juegos o ver más proyectos en mi pagina web de mi "empresa", puedes encontrarme aquí:

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/david-jimenez-villena/)
[![GitHub](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/djsekai34)
[![Web](https://img.shields.io/badge/Página_Web-4285F4?style=for-the-badge&logo=google-chrome&logoColor=white)](https://afterbit.vercel.app)
