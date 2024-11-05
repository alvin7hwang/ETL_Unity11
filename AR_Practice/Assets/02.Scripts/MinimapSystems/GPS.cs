using UnityEngine;
using UnityEngine.Android;
using System.Collections;

namespace ARP.MinimapSystems
{
    /// <summary>
    /// ����� GPS ����
    /// </summary>
    public class GPS : MonoBehaviour, IGPS
    {
        public float latitude => _latitude;

        public float longitude => _longitude;

        public float altitude => _altitude;


        private float _maxWaitTime = 10.0f; // GPS Ÿ�Ӿƿ� ���� Ÿ�̸�
        private float _resendTime = 1.0f; // GPS �����ֱ�
        private float _latitude = 0;
        private float _longitude = 0;
        private float _altitude = 0;
        private float _waitTime = 0;
        private bool _receiveGPS = false;


        void Start()
        {
            StartCoroutine(C_RefreshGPSData());
        }

        /// <summary>
        /// GPS ���� ����
        /// </summary>
        IEnumerator C_RefreshGPSData()
        {
            //����,GPS��� �㰡�� ���� ���ߴٸ�, ���� �㰡 �˾��� ���
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
                while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
                {
                    yield return null;
                }
            }

            //���� GPS ��ġ�� ���� ���� ������ ��ġ ������ ������ �� ���ٰ� ǥ��
            if (!Input.location.isEnabledByUser)
            {
                
                yield break;
            }

            //��ġ �����͸� ��û -> ���� ���
            Input.location.Start();

            //GPS ���� ���°� �ʱ� ���¿��� ���� �ð� ���� �����
            while (Input.location.status == LocationServiceStatus.Initializing && _waitTime < _maxWaitTime)
            {
                yield return new WaitForSeconds(1.0f);
                _waitTime++;
            }

            //���� ���� �� ������ ���еƴٴ� ���� ���
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                
            }

            //���� ��� �ð��� �Ѿ���� ������ �����ٸ� �ð� �ʰ������� ���
            if (_waitTime >= _maxWaitTime)
            {
                
            }

            //���ŵ� GPS �����͸� ȭ�鿡 ���
            LocationInfo li = Input.location.lastData;

            //��ġ ���� ���� ���� üũ
            _receiveGPS = true;

            //��ġ ������ ���� ���� ���� resendTime ������� ��ġ ������ �����ϰ� ���
            while (_receiveGPS)
            {
                li = Input.location.lastData;
                _latitude = li.latitude;
                _longitude = li.longitude;
                _altitude = li.altitude;
                yield return new WaitForSeconds(_resendTime);
            }
        }
    }
}