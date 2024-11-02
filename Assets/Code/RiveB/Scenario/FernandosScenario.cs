using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.RiveB.Scenario
{
    public class FernandosScenario : MonoBehaviour
    {
        public TMP_Text titlePrefab;

        private void Start()
        {
            StartCoroutine(StartScenario());   
        }

        public IEnumerator StartScenario()
        {
            yield return new WaitForEndOfFrame();

            titlePrefab.text = "Ouf je me suis échapé..";

            yield return new WaitForSeconds(2f);

            titlePrefab.text = "Je dois.. trouver quelqu'un...";

            yield return new WaitForSeconds(3f);

            titlePrefab.text = "Mais qui... ?";

            yield return new WaitForSeconds(1.5f);

            titlePrefab.text = string.Empty;
        }

        public IEnumerator EndScenario()
        {
            yield return new WaitForEndOfFrame();

            titlePrefab.text = "Vous avez rendu service à Fernando..";

            yield return new WaitForSeconds(3);

            titlePrefab.text = "Attention je l'entend arriver...";

            yield return new WaitForSeconds(3);

            titlePrefab.text = string.Empty;

            yield return new WaitForSeconds(1);

            titlePrefab.text = "3...";

            yield return new WaitForSeconds(1);

            titlePrefab.text = "2..";

            yield return new WaitForSeconds(1);

            titlePrefab.text = "1.";

            yield return new WaitForSeconds(1);

            titlePrefab.text = string.Empty;
            SceneManager.LoadScene("RiveB2");
        }
    }
}