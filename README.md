# Ecommerce Web API 🛒

Proste Web API napisane w technologii .NET służące do zarządzania zamówieniami oraz produktami w systemie e-commerce. Projekt korzysta z bazy danych SQLite oraz Entity Framework Core i implementuje relację wiele-do-wielu między produktami a zamówieniami.

---

## Dokumentacja Procesu CI/CD (Automatyzacja)

W projekcie skonfigurowano proces ciągłej integracji (**CI - Continuous Integration**) za pomocą narzędzia **GitHub Actions**.

### Jak to działa?
Każda zmiana w kodzie (komenda `git push`) wysłana na główną gałąź (`main`) automatycznie uruchamia proces testowy na serwerach GitHub. Dzięki temu mamy pewność, że nowo dodany kod nie "psuje" aplikacji i poprawnie się kompiluje.

### Kroki wykonywane przez serwer:
1. **Wyzwalacz (Trigger):** Wykrycie zdarzenia `push` na gałęzi `main`.
2. **Środowisko:** Uruchomienie czystej maszyny wirtualnej z systemem Linux (Ubuntu).
3. **Pobranie kodu:** Klonowanie repozytorium do pamięci maszyny wirtualnej (`actions/checkout`).
4. **Instalacja SDK:** Przygotowanie środowiska uruchomieniowego .NET.
5. **Przywracanie paczek (`dotnet restore`):** Pobranie wszystkich bibliotek zewnętrznych potrzebnych do działania aplikacji.
6. **Budowanie (`dotnet build`):** Kompilacja projektu w konfiguracji produkcyjnej (`Release`). Jeśli w kodzie pojawią się błędy składniowe, ten etap przerwie proces i powiadomi o błędzie.

### Status Budowania (Badge)
Możesz śledzić status ostatniego uruchomienia workflow bezpośrednio w zakładce **Actions** na swoim profilu GitHub.

---

## Wdrożenie w Chmurze (Alternatywne środowisko - Render)

Aplikacja została pomyślnie wdrożona i udostępniona publicznie z pominięciem platformy Microsoft Azure.

### Jak się połączyć z wdrożoną aplikacją?
Główny adres URL wdrożonej aplikacji:  
`https://moje-ecommerce-api.onrender.com` *(Pamiętaj, aby wkleić tu swój link!)*

Dokumentacja interfejsu API (Swagger):  
👉 `https://moje-ecommerce-api.onrender.com/swagger/index.html`

### Wykorzystane usługi:
1. **Render Web Services:** Platforma chmurowa typu PaaS (Platform as a Service), która automatycznie uruchamia aplikację na bazie dostarczonego pliku `Dockerfile`. Obsługuje ona darmowy proces hostingu i automatycznie skaluje aplikację do zera w przypadku braku ruchu (co oszczędza zasoby).

### Informacje konfiguracyjne:
* **Konteneryzacja:** Aplikacja została w pełni skonteneryzowana przy użyciu **Dockera**, co uniezależnia ją od konkretnego dostawcy chmurowego (kod zadziała tak samo na Render, AWS czy lokalnym serwerze).
* **Automatyczne wdrożenie (CD):** Pipeline w GitHub Actions korzysta z mechanizmu **Deploy Hooks**. Po każdym udanym teście i zbudowaniu kodu na gałęzi `main`, GitHub wysyła powiadomienie do platformy Render, która w bezpieczny sposób aktualizuje działającą aplikację bez przestojów.

---

## Wdrożenie w Chmurze (Alternatywne środowisko - Render)

Aplikacja została pomyślnie wdrożona i udostępniona publicznie z pominięciem platformy Microsoft Azure.

### Jak się połączyć z wdrożoną aplikacją?
Główny adres URL wdrożonej aplikacji:  
`https://moje-ecommerce-api.onrender.com` *(Pamiętaj, aby wkleić tu swój link!)*

Dokumentacja interfejsu API (Swagger):  
👉 `https://moje-ecommerce-api.onrender.com/swagger/index.html`

### Wykorzystane usługi:
1. **Render Web Services:** Platforma chmurowa typu PaaS (Platform as a Service), która automatycznie uruchamia aplikację na bazie dostarczonego pliku `Dockerfile`. Obsługuje ona darmowy proces hostingu i automatycznie skaluje aplikację do zera w przypadku braku ruchu (co oszczędza zasoby).

### Informacje konfiguracyjne:
* **Konteneryzacja:** Aplikacja została w pełni skonteneryzowana przy użyciu **Dockera**, co uniezależnia ją od konkretnego dostawcy chmurowego (kod zadziała tak samo na Render, AWS czy lokalnym serwerze).
* **Automatyczne wdrożenie (CD):** Pipeline w GitHub Actions korzysta z mechanizmu **Deploy Hooks**. Po każdym udanym teście i zbudowaniu kodu na gałęzi `main`, GitHub wysyła powiadomienie do platformy Render, która w bezpieczny sposób aktualizuje działającą aplikację bez przestojów.

---

## Infrastruktura jako Kod (IaC) za pomocą Azure Bicep

W projekcie wdrożono podejście **Infrastructure as Code (IaC)**. Cała niezbędna infrastruktura sieciowa i serwerowa została opisana w sposób deklaratywny za pomocą języka **Azure Bicep**.

### Wykorzystane pliki:
* `/Infrastructure/main.bicep` – główny skrypt konfiguracyjny automatycznie tworzący dedykowany plan taryfowy (`App Service Plan` w darmowej wersji `F1`) oraz instancję serwera `Web App` przygotowaną pod środowisko uruchomieniowe .NET 8.

### Uruchomienie z poziomu Pipeline'a (CI/CD):
Skrypt Bicep **nie jest** uruchamiany ręcznie przez programistę. Proces ten został w pełni zautomatyzowany w ramach pipeline'u GitHub Actions (`.github/workflows/dotnet-ci.yml`):
1. Po wykryciu zmian na gałęzi `main`, GitHub Actions bezpiecznie loguje się do platformy Azure za pomocą tokenu uwierzytelniającego (`AZURE_CREDENTIALS`).
2. Krok `azure/arm-deploy@v2` analizuje plik `main.bicep`.
3. Chmura Azure porównuje stan faktyczny z kodem. Jeśli serwer jeszcze nie istnieje – zostanie stworzony. Jeśli istnieje – konfiguracja zostanie zweryfikowana i ewentualnie zaktualizowana bez usuwania danych (operacja idempotentna).