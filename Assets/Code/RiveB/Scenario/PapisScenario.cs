using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.RiveB.Scenario
{
    public class PapisScenario : MonoBehaviour
    {
        public TMP_Text titlePrefab;
        private Color maman = new Color(0f, 1f, 170f / 255f);
        private Color papi = new Color(125f / 255f, 0f, 1f);

        private void Start()
        {
            StartCoroutine(StartScenario());
        }

        public IEnumerator StartScenario()
        {
            yield return new WaitForEndOfFrame();

            titlePrefab.color = maman;
            titlePrefab.text = "Je suis déjà venu par ici..";

            yield return new WaitForSeconds(2f);

            titlePrefab.text = "Je vais peut-être le revoir !";

            yield return new WaitForSeconds(2.5f);

            titlePrefab.text = string.Empty;
        }

        public IEnumerator EndScenario()
        {
            yield return new WaitForEndOfFrame();

            titlePrefab.color = maman;
            titlePrefab.text = "Où es-tu.. ?";

            yield return new WaitForSeconds(1.5f);

            titlePrefab.color = papi;
            titlePrefab.text = "Je suis là, je suis toujours avec toi..";

            yield return new WaitForSeconds(6f);

            titlePrefab.color = maman;
            titlePrefab.text = "Tu es là.. ?";

            yield return new WaitForSeconds(3f);

            titlePrefab.color = papi;
            titlePrefab.text = "Oui, promis.";

            yield return new WaitForSeconds(2f);

            titlePrefab.color = Color.white;
            titlePrefab.text = "Un nouveau départ dans..";

            yield return new WaitForSeconds(3f);

            titlePrefab.text = "3...";

            yield return new WaitForSeconds(1f);

            titlePrefab.text = "2..";

            yield return new WaitForSeconds(1f);

            titlePrefab.text = "1.";

            yield return new WaitForSeconds(1f);

            titlePrefab.color = papi;
            titlePrefab.text = "Je t'aime ma chérie.";
            
            yield return new WaitForSeconds(5f);

            titlePrefab.color = Color.white;
            titlePrefab.text = "0";

            yield return new WaitForSeconds(0.25f);

            titlePrefab.text = string.Empty;
            SceneManager.LoadScene("Limoges");
        }
    }
}