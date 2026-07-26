# 🛡️ Auth-Server — Servidor Central de Identidad y Orquestación TConecta

El **Auth-Server** es el núcleo central de identidad, emisión de tokens JWT, control de acceso y orquestación del ecosistema **TConecta**. Está desarrollado sobre **.NET 8 Web API** siguiendo **Clean Architecture** y actúa como el único proveedor de autenticación para todas las aplicaciones web y móviles del sistema.

---

## 🏗️ Arquitectura General del Ecosistema

```
                             ┌──────────────────────────────────┐
                             │       Auth-Server (.NET 8)       │
                             │  http://localhost:8080/swagger   │
                             └────────────────┬─────────────────┘
                                              │
                      ┌───────────────────────┼───────────────────────┐
                      │                       │                       │
                      ▼                       ▼                       ▼
           ┌─────────────────────┐ ┌─────────────────────┐ ┌─────────────────────┐
           │    Server-Admin     │ │    Server-Client    │ │     Server-User     │
           │ Node.js (Port 3001) │ │ Node.js (Port 3002) │ │ Node.js (Port 3003) │
           └──────────┬──────────┘ └──────────┬──────────┘ └──────────┬──────────┘
                      │                       │                       │
                      ▼                       ▼                       ▼
           ┌─────────────────────┐ ┌─────────────────────┐ ┌─────────────────────┐
           │    Client-Admin     │ │     Client-User     │ │ Client-User-Mobile  │
           │  React (Port 5173)  │ │  React (Port 5174)  │ │ Expo (React Native) │
           └─────────────────────┘ └─────────────────────┘ └─────────────────────┘
```

---

## 🔑 Credenciales Predeterminadas y Roles

Al aplicar las migraciones de Entity Framework Core, la base de datos PostgreSQL se inicializa automáticamente con las siguientes cuentas pre-sembradas:

| Rol | CUI / DPI | Correo Electrónico | Contraseña | Destino de Inicio de Sesión |
|---|---|---|---|---|
| **Administrador** | `1000000000001` | `admin@tconecta.com` | `Admin123!` | `http://localhost:5173/auth` |
| **Administrador** | `0000000000000` | `admin@transmetro.com` | `AdminTransmetro2026!` | `http://localhost:5173/auth` |
| **Ciudadano** | `2000000000002` | `usuario@correo.com` | `Usuario123!` | `http://localhost:5174/auth` / App Expo |

---

## 🐋 🚀 Despliegue Completo con Docker Compose

El archivo de orquestación `docker-compose.yml` se encuentra ubicado dentro de este repositorio (`Auth-Server/docker-compose.yml`).

### Pasos para Levantar Todo el Sistema

1. Abre la terminal e ingresa al directorio de **Auth-Server**:
   ```bash
   cd C:\Repositorios\TConecta\Auth-Server
   ```

2. Ejecuta el comando para construir y desplegar todos los servicios:
   ```bash
   docker compose up --build -d
   ```

3. Comprueba el estado de los contenedores:
   ```bash
   docker compose ps
   ```

---

## 🌐 Mapa de Servicios y Endpoints

| Servicio | Tecnología | Puerto | URL Base |
|---|---|---|---|
| **Auth-Server** | .NET 8 Web API | `8080` | `http://localhost:8080/swagger` |
| **Server-Admin** | Node.js Express | `3001` | `http://localhost:3001/TCONECTA/v1` |
| **Server-Client** | Node.js Express | `3002` | `http://localhost:3002/TRANSMETRO-CONECTA-CLIENTE/v1` |
| **Server-User** | Node.js Express | `3003` | `http://localhost:3003/TRANSMETRO-CONECTA-USUARIO/v1` |
| **Client-Admin** | React + Vite (Nginx) | `5173` | `http://localhost:5173/auth` |
| **Client-User** | React + Vite (Nginx) | `5174` | `http://localhost:5174/auth` |
| **Client-User-Mobile** | Expo React Native | N/A | Metro Bundler (`npx expo start`) |
| **PostgreSQL** | Relacional | `5432` | `localhost:5432` (`TransmetroAuthDb`) |
| **MongoDB** | NoSQL | `27017` | `localhost:27017` (`TransmetroAdminDb`, `TransmetroUserDb`) |

---

## 🧪 Guía Paso a Paso para Probar todo el Flujo

### 🅰️ 1. Flujo Administrativo (Client-Admin)
1. Navega a **`http://localhost:5173/auth`**.
2. **Iniciar Sesión**: Ingresa con CUI `1000000000001` y contraseña `Admin123!`.
3. **Tablero de Control (`/dashboard`)**: Revisa los contadores de infraestructura y boletines.
4. **Gestión de Rutas (`/dashboard/roads`)**: Crea una ruta `L12` (Tipo `CENTRALES`, Coordenadas `-90.5350, 14.6150\n-90.5130, 14.6400`) y cambia su estado.
5. **Estaciones (`/dashboard/stations`)**: Crea una nueva estación `EST-12` (`El Trébol`).
6. **Boletines de Alerta (`/dashboard/alerts`)**: Emite un boletín de mantenimiento y márcalo como resuelto.
7. **Padrón de Usuarios (`/dashboard/users`)**: Consulta el listado de usuarios de Auth-Server.

---

### 🅱️ 2. Flujo Web Ciudadano (Client-User)
1. Navega a **`http://localhost:5174/auth`**.
2. **Iniciar Sesión / Registro**: Registra una nueva cuenta o ingresa con CUI `2000000000002` y contraseña `Usuario123!`.
3. **Billetera Ciudadana (`/wallet`)**:
   - **Compra de Tarjeta Ciudadana (Q20.00)**: Ingresa un número de tarjeta bancaria válido por Luhn (`4532015112830366`), Expiración `12/28` y CVV `123`. Al completarse la compra S2S, se acreditarán **5 viajes de cortesía**.
   - **Recarga de Saldo Virtual**: Selecciona el monto `Q50.00` y recarga.
4. **Planificador Multimodal (`/planner`)**:
   - Elige un preset de ruta o coordenadas personalizadas.
   - Selecciona el modo: Transmetro (`Q1.00`), TuBus (`Q1.00`) o Transurbano (`Q2.00`).
   - Haz clic en **"Calcular Ruta y Debitar Pasaje"**. Verifica la distancia Haversine, tiempo estimado, itinerario y deducción de saldo.
5. **Feed de Alertas (`/alerts`)**: Consulta las alertas operativas emitidas por los administradores.

---

### 📱 3. Flujo Móvil Android (Client-User-Mobile con Expo)
1. Abre una terminal e ingresa al repositorio móvil:
   ```bash
   cd C:\Repositorios\TConecta\Client-User-Mobile
   ```
2. Ejecuta Expo CLI:
   ```bash
   npx expo start
   ```
3. Escanea el código QR desde la aplicación **Expo Go** en Android o presiona `a` para abrir en el emulador.
4. Ingresa con CUI `2000000000002` / `Usuario123!` para consultar saldo, recargar y planificar viajes en tiempo real.